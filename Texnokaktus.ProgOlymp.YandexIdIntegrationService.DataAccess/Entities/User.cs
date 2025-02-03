namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Entities;

public class User
{
    public int Id { get; init; }
    public string Login { get; init; }
    public string? DisplayName { get; set; }
    public bool? IsAvatarEmpty { get; set; }
    public string? AvatarId { get; set; }
    // public string AccessToken { get; set; }
    // public string? RefreshToken { get; set; }
    // public DateTimeOffset AccessTokenExpiration { get; set; }
}
