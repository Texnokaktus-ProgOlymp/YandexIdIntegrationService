using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Context;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories.Abstractions;
using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Serivces.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Serivces;

public class UnitOfWork(AppDbContext context, IUserRepository userRepository) : IUnitOfWork
{
    public IUserRepository UserRepository { get; } = userRepository;

    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
