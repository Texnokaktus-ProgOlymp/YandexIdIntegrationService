using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;
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

        return user;
    }
}
