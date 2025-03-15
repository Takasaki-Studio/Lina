using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database.Repositories;

/// <summary>
/// Base repository
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TPkType">Entity id</typeparam>
public abstract class BaseRepository<TEntity, TPkType> : IBaseRepository<TEntity, TPkType>
    where TEntity : class, IBaseEntity<TPkType>
{
    protected readonly DbContext DbContext;

    protected BaseRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns>Returns the entity that has the id or null if it does not exist</returns>
    public async Task<TEntity?> GetById(TPkType id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Get entity by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns the first entity found by the filter or null if it does not exist</returns>
    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
    }

    /// <summary>
    /// Get entities by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns the entities found by the filter</returns>
    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        return await DbContext.Set<TEntity>().Where(expression).ToListAsync();
    }

    /// <summary>
    /// Verify if entity exists
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns if entity exists</returns>
    public async ValueTask<bool> Exists(Expression<Func<TEntity, bool>> expression)
    {
        return await DbContext.Set<TEntity>().AnyAsync(expression);
    }

    /// <summary>
    /// Get number of registers by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Return number of register by filter</returns>
    public async ValueTask<int> Count(Expression<Func<TEntity, bool>> expression)
    {
        return await DbContext.Set<TEntity>().CountAsync(expression);
    }

    /// <summary>
    /// Get number of registers
    /// </summary>
    /// <returns>Return number of register</returns>
    public async ValueTask<int> Count()
    {
        return await DbContext.Set<TEntity>().CountAsync();
    }

    /// <summary>
    /// Add entity in database
    /// </summary>
    /// <param name="item">Entity to add</param>
    public async ValueTask Add(TEntity item)
    {
        await DbContext.Set<TEntity>().AddAsync(item);
    }

    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="newItem">Entity to update</param>
    public void Update(TEntity newItem)
    {
        DbContext.Set<TEntity>().Update(newItem);
    }

    /// <summary>
    /// Delete entity from database
    /// </summary>
    /// <param name="item">Entity to delete</param>
    public void Delete(TEntity item)
    {
        DbContext.Set<TEntity>().Remove(item);
    }

    /// <summary>
    /// Effective changes to the database
    /// </summary>
    /// <returns>Rows affects</returns>
    public async ValueTask<int> Commit()
    {
        return await DbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Create database transaction
    /// </summary>
    /// <returns>Transaction context</returns>
    public Task<IDbContextTransaction> BeginTransaction()
    {
        return DbContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Base function for clear all entities in tracking
    /// </summary>
    public void DetachAllEntities()
    {
        DbContext.ChangeTracker.Clear();
    }

    /// <summary>
    /// Base function for remove entity in tracking
    /// </summary>
    /// <param name="entity">Entity for remove</param>
    public void Detach(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Detached;
    }

    /// <summary>
    /// Add or update entity in database based on Id value
    /// </summary>
    /// <param name="entity">Entity to add or update</param>
    /// <returns>Async operation</returns>
    public async ValueTask AddOrUpdate(TEntity entity)
    {
        if (EqualityComparer<TPkType>.Default.Equals(entity.Id))
        {
            await Add(entity);
            return;
        }

        Update(entity);
    }
}