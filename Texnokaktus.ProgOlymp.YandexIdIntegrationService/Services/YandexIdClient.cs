using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Exceptions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Models;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Services;

internal class YandexIdClient(HttpClient client) : IYandexIdClient
{
    public async Task<UserData> GetUserDataAsync(string accessToken)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "info")
        {
            Headers =
            {
                Authorization = new("OAuth", accessToken)
            }
        };

        var responseMessage = await client.SendAsync(requestMessage);
        responseMessage.EnsureSuccessStatusCode();

        return await responseMessage.Content.ReadFromJsonAsync<UserData>()
            ?? throw new YandexApiException("Invalid data from Yandex ID server");
    }
}
