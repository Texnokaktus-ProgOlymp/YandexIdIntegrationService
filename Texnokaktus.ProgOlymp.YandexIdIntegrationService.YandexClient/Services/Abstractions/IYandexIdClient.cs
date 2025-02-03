using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Models;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;

public interface IYandexIdClient
{
    Task<UserData> GetUserDataAsync(string accessToken);
}
