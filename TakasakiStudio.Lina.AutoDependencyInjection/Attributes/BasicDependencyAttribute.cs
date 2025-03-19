using TakasakiStudio.Lina.AutoDependencyInjection.Interfaces;

namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Base attribute for implement other attributes 
/// </summary>
public abstract class BasicDependencyAttribute(DependencyType dependencyType = DependencyType.Normal)
    : Attribute, IDynamicServicesProviderAttribute
{
    public abstract LifeTime LifeTime { get; }
    public abstract Type? Interface { get; }
    public DependencyType DependencyType { get; } = dependencyType;
}

/// <summary>
/// Base attribute for implement other attributes 
/// </summary>
public abstract class BasicDependencyAttribute<T>(DependencyType dependencyType = DependencyType.Normal)
    : Attribute, IDynamicServicesProviderAttribute
{
    public abstract LifeTime LifeTime { get; }
    public Type? Interface { get; } = typeof(T);
    public DependencyType DependencyType { get; } = dependencyType;
}