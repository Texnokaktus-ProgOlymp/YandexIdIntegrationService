using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Serivces;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Serivces.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess;

public static class DiExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection,
                                                   Action<DbContextOptionsBuilder> optionsAction) =>
        serviceCollection.AddDbContext<AppDbContext>(optionsAction)
                         .AddScoped<IUnitOfWork, UnitOfWork>()
                         .AddScoped<IUserRepository, UserRepository>();

    public static IHealthChecksBuilder AddDatabaseHealthChecks(this IHealthChecksBuilder builder) =>
        builder.AddDbContextCheck<AppDbContext>("database");
}
