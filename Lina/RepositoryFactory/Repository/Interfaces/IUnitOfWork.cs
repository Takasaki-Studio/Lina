using Microsoft.EntityFrameworkCore.Storage;

namespace Lina.RepositoryFactory.Repository.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges();
    Task<IDbContextTransaction> Begin();
    void ClearContext();
}