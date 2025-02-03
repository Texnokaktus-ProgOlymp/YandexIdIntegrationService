using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Domain;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;

public interface IUserDataService
{
    Task<User> AuthenticateUserAsync(string code);
    Task<User?> GetUserInfoAsync(string login);
}
