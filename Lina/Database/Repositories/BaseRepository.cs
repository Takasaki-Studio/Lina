using System.Linq.Expressions;
using Lina.Database.Interfaces;
using Lina.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lina.Database.Repositories;

/// <summary>
/// Base repository
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TPkType">Entity id</typeparam>
public abstract class BaseRepository<TEntity, TPkType> : IBaseRepository<TEntity, TPkType>
    where TEntity : BaseEntity<TPkType>
{
    private readonly DbContext _dbContext;

    protected BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns>Returns the entity that has the id or null if it does not exist</returns>
    public async Task<TEntity?> GetById(TPkType id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Get entity by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns the first entity found by the filter or null if it does not exist</returns>
    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(expression);
    }

    /// <summary>
    /// Get entities by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns the entities found by the filter</returns>
    public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().Where(expression).ToListAsync();
    }

    /// <summary>
    /// Verify if entity exists
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns if entity exists</returns>
    public async ValueTask<bool> Exists(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(expression);
    }

    /// <summary>
    /// Add entity in database
    /// </summary>
    /// <param name="item">Entity to add</param>
    public async ValueTask Add(TEntity item)
    {
        await _dbContext.Set<TEntity>().AddAsync(item);
    }

    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="newItem">Entity to update</param>
    public void Update(TEntity newItem)
    {
        _dbContext.Set<TEntity>().Update(newItem);
    }

    /// <summary>
    /// Delete entity from database
    /// </summary>
    /// <param name="item">Entity to delete</param>
    public void Delete(TEntity item)
    {
        _dbContext.Set<TEntity>().Remove(item);
    }

    /// <summary>
    /// Effective changes to the database
    /// </summary>
    /// <returns>Rows affects</returns>
    public async ValueTask<int> Commit()
    {
        return await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Create database transaction
    /// </summary>
    /// <returns>Transaction context</returns>
    public Task<IDbContextTransaction> BeginTransaction()
    {
        return _dbContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// Base function for clear all entities in tracking
    /// </summary>
    public void DetachAllEntities()
    {
        _dbContext.ChangeTracker.Clear();
    }

    /// <summary>
    /// Base function for remove entity in tracking
    /// </summary>
    /// <param name="entity">Entity for remove</param>
    public void Detach(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
    }
}