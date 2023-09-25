namespace Lina.DynamicServicesProvider;

/// <summary>
/// Service lifetime
/// </summary>
public enum LifeTime
{
    /// <summary>
    /// Create a new instance for DI injection
    /// </summary>
    Transient,
    /// <summary>
    /// Create a new instance for scope
    /// </summary>
    Scoped,
    /// <summary>
    /// Create a single instance for the entire project
    /// </summary>
    Singleton
}