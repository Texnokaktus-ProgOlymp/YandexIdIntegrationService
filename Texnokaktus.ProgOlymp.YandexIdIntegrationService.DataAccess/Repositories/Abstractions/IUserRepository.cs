using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Entities;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories.Abstractions;

public interface IUserRepository
{
    Task<User?> GetUserByLoginAsync(string login);
    Task<User?> GetUserByLoginTrackedAsync(string login);
    User AddUser(UserInsertModel insertModel);
}
