using System.Linq.Expressions;
using Lina.Database.Interfaces;
using Lina.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lina.Database.Repositories;

public abstract class BaseRepository<TEntity, TPkType> : IBaseRepository<TEntity, TPkType>
    where TEntity : BaseEntity<TPkType>
{
    private readonly DbContext _dbContext;

    protected BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetById(TPkType id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
    }

    public async ValueTask<bool> Exists(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(expression);
    }

    public async ValueTask Add(TEntity item)
    {
        await _dbContext.Set<TEntity>().AddAsync(item);
    }

    public void Update(TEntity newItem)
    {
        _dbContext.Set<TEntity>().Update(newItem);
    }

    public void Delete(TEntity item)
    {
        _dbContext.Set<TEntity>().Remove(item);
    }

    public async ValueTask<int> Commit()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public Task<IDbContextTransaction> BeginTransaction()
    {
        return _dbContext.Database.BeginTransactionAsync();
    }

    public void DetachAllEntities()
    {
        _dbContext.ChangeTracker.Clear();
    }

    public void Detach(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
    }
}