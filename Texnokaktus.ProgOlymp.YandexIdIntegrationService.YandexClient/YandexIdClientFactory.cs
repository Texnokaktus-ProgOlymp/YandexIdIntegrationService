using RestSharp;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;

internal delegate IRestClient YandexIdClientFactory(string accessToken);
