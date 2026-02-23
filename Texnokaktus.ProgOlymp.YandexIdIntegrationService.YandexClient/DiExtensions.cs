using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;
using YandexOAuthClient;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;

public static class DiExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddYandexClient()
        {
            services.AddOAuthClient()
                    .AddHttpClient<IYandexIdClient, YandexIdClient>(client => client.BaseAddress = new("https://login.yandex.ru"));

            return services;
        }
    }
}
