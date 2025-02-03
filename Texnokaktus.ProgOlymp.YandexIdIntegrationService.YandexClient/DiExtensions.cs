using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Models;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Options;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient;

public static class DiExtensions
{
    public static IServiceCollection AddYandexClient(this IServiceCollection services) =>
        services.AddScoped<IYandexAuthenticationService, YandexAuthenticationService>()
                .AddScoped<IYandexIdClient, YandexIdClient>()
                .AddOAuthClient()
                .AddYandexIdClient()
                .AddYandexAppParameters();

    private static IServiceCollection AddYandexAppParameters(this IServiceCollection services)
    {
        services.AddOptions<YandexAppParameters>().BindConfiguration(nameof(YandexAppParameters));
        return services;
    }

    private static IServiceCollection AddOAuthClient(this IServiceCollection services) =>
        services.AddKeyedScoped<IAuthenticator>(ClientType.YandexOAuth,
                                                (provider, _) =>
                                                {
                                                    var (clientId, clientSecret) = provider.GetRequiredService<IOptions<YandexAppParameters>>().Value;
                                                    return new HttpBasicAuthenticator(clientId, clientSecret);
                                                })
                .AddKeyedScoped<IRestClient>(ClientType.YandexOAuth,
                                             (provider, key) =>
                                             {
                                                 var authenticator = provider.GetRequiredKeyedService<IAuthenticator>(key);
                                                 return new RestClient("https://oauth.yandex.ru",
                                                                       options => options.Authenticator = authenticator);
                                             });

    private static IServiceCollection AddYandexIdClient(this IServiceCollection services) =>
        services.AddKeyedScoped<YandexIdClientFactory>(ClientType.YandexId,
                                                       (_, _) => token =>
                                                       {
                                                           var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);
                                                           return new RestClient("https://login.yandex.ru",
                                                                                 options => options.Authenticator = authenticator);
                                                       });
}
