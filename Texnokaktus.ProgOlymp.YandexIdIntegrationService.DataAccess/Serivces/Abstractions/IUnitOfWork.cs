using Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.YandexIdIntegrationService.DataAccess.Serivces.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync();
}
