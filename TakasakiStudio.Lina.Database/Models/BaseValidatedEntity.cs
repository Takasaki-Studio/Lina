using TakasakiStudio.Lina.Common;
using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database.Models;

/// <summary>
/// Base entity with validation
/// </summary>
/// <typeparam name="TModel">Entity model</typeparam>
/// <typeparam name="TPkKey">Entity id type</typeparam>
public abstract class BaseValidatedEntity<TModel, TPkKey> : BaseValidated<TModel>, IBaseEntity<TPkKey>
{
    /// <summary>
    /// Entity id
    /// </summary>
    public TPkKey Id { get; set; } = default!;
    
    /// <summary>
    /// Create a clone of value
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    /// <returns></returns>
    public T Clone<T>()
    {
        return (T)MemberwiseClone();
    }
}