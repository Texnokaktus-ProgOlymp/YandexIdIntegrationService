using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;
using YandexOAuthClient;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;

public static class DiExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddYandexClient() =>
            services.AddOAuthClient()
                    .AddYandexIdClient();

        private IServiceCollection AddYandexIdClient() =>
            services.AddScoped<IYandexIdClient, YandexIdClient>()
                    .AddScoped<YandexIdClientFactory>(_ => token =>
                     {
                         var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);
                         return new RestClient("https://login.yandex.ru", options => options.Authenticator = authenticator);
                     });
    }
}
