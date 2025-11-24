using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;

public interface IUserDataService
{
    Task<User> AuthenticateUserAsync(string code);
    Task<User?> GetUserInfoAsync(string login);
}
