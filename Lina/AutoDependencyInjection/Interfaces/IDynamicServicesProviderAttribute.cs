namespace Lina.AutoDependencyInjection.Interfaces;

/// <summary>
/// Base interface for get dependency injection information
/// </summary>
public interface IDynamicServicesProviderAttribute
{
    /// <summary>
    /// Dependency life time
    /// </summary>
    LifeTime LifeTime { get; }
    
    /// <summary>
    /// Interface reference type
    /// </summary>
    Type? Interface { get; }
    
    /// <summary>
    /// Dependency type
    /// </summary>
    DependencyType DependencyType { get; }
}