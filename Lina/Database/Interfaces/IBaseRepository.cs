using System.Linq.Expressions;
using Lina.Database.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lina.Database.Interfaces;

public interface IBaseRepository<TEntity, in TPkType>
    where TEntity : BaseEntity<TPkType>
{
    Task<TEntity?> GetById(TPkType id);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);
    ValueTask<bool> Exists(Expression<Func<TEntity, bool>> expression);
    ValueTask Add(TEntity item);
    void Update(TEntity newItem);
    void Delete(TEntity item);
    ValueTask<int> Commit();
    Task<IDbContextTransaction> BeginTransaction();
    void DetachAllEntities();
    void Detach(TEntity entity);
}