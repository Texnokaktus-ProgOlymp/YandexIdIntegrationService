namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Models;

public record UserInsertModel(string Login, string? DisplayName, bool? IsAvatarEmpty, string? AvatarId);
