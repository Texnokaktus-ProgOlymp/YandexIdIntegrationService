using Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Models;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Services.Abstractions;

public interface IYandexAuthenticationService
{
    string GetYandexOAuthUrl(string? localRedirectUri);
    Task<TokenResponse> GetAccessTokenAsync(string code);
    Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken);
}
