namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Entities;

public class User
{
    public int Id { get; init; }
    public string Login { get; init; }
    public string AvatarId { get; set; }
    // public string AccessToken { get; set; }
    // public string? RefreshToken { get; set; }
    // public DateTimeOffset AccessTokenExpiration { get; set; }
}
