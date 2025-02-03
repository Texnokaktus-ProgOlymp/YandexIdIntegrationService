using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.Logic;

public static class DiExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services) =>
        services.AddScoped<IUserDataService, UserDataService>();
}
