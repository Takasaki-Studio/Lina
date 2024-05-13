using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database.Models;

/// <summary>
/// Base entity database 
/// </summary>
/// <typeparam name="TPkType">Entity id type</typeparam>
public abstract class BaseEntity<TPkType> : IBaseEntity<TPkType>
{
    /// <summary>
    /// Entity id
    /// </summary>
    public TPkType Id { get; set; } = default!;

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