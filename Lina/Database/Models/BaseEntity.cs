namespace Lina.Database.Models;

/// <summary>
/// Base entity database 
/// </summary>
/// <typeparam name="TPkType">Entity id type</typeparam>
public abstract class BaseEntity<TPkType>
{
    /// <summary>
    /// Entity id
    /// </summary>
    public TPkType Id { get; set; } = default!;
}