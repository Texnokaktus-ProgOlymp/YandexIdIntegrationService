namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Exceptions;

public class YandexAuthenticationException : YandexApiException
{
    internal YandexAuthenticationException(string? message) : base(message)
    {
    }

    internal YandexAuthenticationException(string? message, Exception innerException) : base(message, innerException)
    {
    }
}
