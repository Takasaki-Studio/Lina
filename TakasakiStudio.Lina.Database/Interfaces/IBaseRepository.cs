using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using TakasakiStudio.Lina.Database.Models;

namespace TakasakiStudio.Lina.Database.Interfaces;

/// <summary>
/// Base repository interface
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
/// <typeparam name="TPkType">Entity id type</typeparam>
public interface IBaseRepository<TEntity, in TPkType>
    where TEntity : BaseEntity<TPkType>
{
    /// <summary>
    /// Base function for find by id
    /// </summary>
    /// <param name="id">Entity id</param>
    /// <returns>Returns the entity that has the id or null if it does not exist</returns>
    Task<TEntity?> GetById(TPkType id);
    
    /// <summary>
    /// Base function for find by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns the first entity found by the filter or null if it does not exist</returns>
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression);
    
    /// <summary>
    /// Base function for get all entities by filter
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns the entities found by the filter</returns>
    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression);
    
    /// <summary>
    /// Base function for verify if entity exists
    /// </summary>
    /// <param name="expression">Expression filter</param>
    /// <returns>Returns if entity exists</returns>
    ValueTask<bool> Exists(Expression<Func<TEntity, bool>> expression);
    
    /// <summary>
    /// Base function for add entity in database
    /// </summary>
    /// <param name="item">Entity to add</param>
    ValueTask Add(TEntity item);
    
    /// <summary>
    /// Base function for update entity in database
    /// </summary>
    /// <param name="newItem">Entity to update</param>
    void Update(TEntity newItem);
    
    /// <summary>
    /// Base function for delete entity in database
    /// </summary>
    /// <param name="item">Entity to delete</param>
    void Delete(TEntity item);
    
    /// <summary>
    /// Base function for effective changes to the database
    /// </summary>
    /// <returns>Rows affects</returns>
    ValueTask<int> Commit();
    
    /// <summary>
    /// Base function for create database transaction
    /// </summary>
    /// <returns>Transaction context</returns>
    Task<IDbContextTransaction> BeginTransaction();
    
    /// <summary>
    /// Base function for clear all entities in tracking
    /// </summary>
    void DetachAllEntities();
    
    /// <summary>
    /// Base function for remove entity in tracking
    /// </summary>
    /// <param name="entity">Entity for remove</param>
    void Detach(TEntity entity);
}