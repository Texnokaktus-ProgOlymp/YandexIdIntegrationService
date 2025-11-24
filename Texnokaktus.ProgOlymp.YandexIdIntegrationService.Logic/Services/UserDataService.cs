using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Domain;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Models;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;
using YandexOAuthClient.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services;

internal class UserDataService(IAuthService authService,
                               IYandexIdClient yandexIdClient,
                               AppDbContext context) : IUserDataService
{
    public async Task<User> AuthenticateUserAsync(string code)
    {
        var token = await authService.GetAccessTokenAsync(code);
        var userData = await yandexIdClient.GetUserDataAsync(token);

        var user = await context.Users.FirstOrDefaultAsync(dbUser => dbUser.Login == userData.Login)
                ?? context.Users
                          .Add(new()
                           {
                               Login = userData.Login
                           })
                          .Entity;

        user.DisplayName = userData.DisplayName;
        user.IsAvatarEmpty = userData.IsAvatarEmpty;
        user.AvatarId = userData.DefaultAvatarId;

        await context.SaveChangesAsync();

        return userData.MapUser();
    }

    public async Task<User?> GetUserInfoAsync(string login)
    {
        var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Login == login);
        return user?.MapUser();
    }
}

file static class MappingExtensions
{
    public static User MapUser(this UserData userData) =>
        new(userData.Login,
            userData.DisplayName,
            userData.IsAvatarEmpty.HasValue
                ? new Avatar(userData.DefaultAvatarId)
                : null);
    
    public static User MapUser(this DataAccess.Entities.User user) =>
        new(user.Login,
            user.DisplayName,
            user.IsAvatarEmpty.HasValue
                ? new Avatar(user.AvatarId)
                : null);
}
