using Microsoft.EntityFrameworkCore;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Models;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories;

internal class UserRepository(AppDbContext context) : IUserRepository
{
    public Task<User?> GetUserByLoginAsync(string login) =>
        context.Users
               .AsNoTracking()
               .FirstOrDefaultAsync(user => user.Login == login);

    public Task<User?> GetUserByLoginTrackedAsync(string login) =>
        context.Users.FirstOrDefaultAsync(user => user.Login == login);

    public User AddUser(UserInsertModel insertModel)
    {
        var entity = new User
        {
            Login = insertModel.Login, AvatarId = insertModel.AvatarId
        };

        return context.Users.Add(entity).Entity;
    }
}
