using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database.Models;

/// <summary>
/// Base entity database 
/// </summary>
/// <typeparam name="TPkType">Entity id type</typeparam>
public abstract class BaseEntity<TPkType> : IBaseEntity<TPkType>
{
    public TPkType Id { get; set; } = default!;
}