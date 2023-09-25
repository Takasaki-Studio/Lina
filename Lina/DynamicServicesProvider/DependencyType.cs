namespace Lina.DynamicServicesProvider;

/// <summary>
/// Injection type
/// </summary>
public enum DependencyType
{
    /// <summary>
    /// Default injection
    /// </summary>
    Normal,
    /// <summary>
    /// Inject with http client
    /// </summary>
    Http,
}