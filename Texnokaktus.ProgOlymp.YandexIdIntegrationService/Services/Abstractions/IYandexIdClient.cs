using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Models;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Abstractions;

public interface IYandexIdClient
{
    Task<UserData> GetUserDataAsync(string accessToken);
}
