using System.Text.Json.Serialization;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Models;

public record UserData
{
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("real_name")]
    public string? RealName { get; set; }

    [JsonPropertyName("is_avatar_empty")]
    public bool? IsAvatarEmpty { get; set; }

    [JsonPropertyName("default_avatar_id")]
    public string? DefaultAvatarId { get; set; }

    [JsonPropertyName("sex")]
    public string? Gender { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }
}
