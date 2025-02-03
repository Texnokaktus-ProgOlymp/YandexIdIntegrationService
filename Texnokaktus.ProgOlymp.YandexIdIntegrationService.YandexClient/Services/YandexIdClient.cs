using RestSharp;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Exceptions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Models;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services;

internal class YandexIdClient(YandexIdClientFactory clientFactory) : IYandexIdClient
{
    public async Task<UserData> GetUserDataAsync(string accessToken)
    {
        var client = clientFactory.Invoke(accessToken);
        
        var request = new RestRequest("info");

        var response = await client.ExecuteGetAsync<UserData>(request);

        if (!response.IsSuccessful)
        {
            if (response.ErrorException is not null)
                throw new YandexApiException("An error occurred while requesting the user data", response.ErrorException);
            throw new YandexApiException("An error occurred while requesting the user data");
        }

        if (response.Data is null)
            throw new YandexApiException("Invalid data from Yandex ID server");

        return response.Data;
    }
}
