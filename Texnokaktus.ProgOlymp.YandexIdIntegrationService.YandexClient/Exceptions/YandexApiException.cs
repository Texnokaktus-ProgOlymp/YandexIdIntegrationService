namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.YandexClient.Exceptions;

public class YandexApiException : Exception
{
    internal YandexApiException(string? message) : base(message)
    {
    }

    internal YandexApiException(string? message, Exception innerException) : base(message, innerException)
    {
    }
}
