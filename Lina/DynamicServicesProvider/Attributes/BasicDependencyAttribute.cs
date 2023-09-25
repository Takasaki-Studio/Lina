using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

/// <summary>
/// Base attribute for implement other attributes 
/// </summary>
public abstract class BasicDependencyAttribute : Attribute, IDynamicServicesProviderAttribute
{
    public abstract LifeTime LifeTime { get; }
    public abstract Type? Interface { get; }
    public DependencyType DependencyType { get; }

    protected BasicDependencyAttribute(DependencyType dependencyType = DependencyType.Normal)
    {
        DependencyType = dependencyType;
    } 
}