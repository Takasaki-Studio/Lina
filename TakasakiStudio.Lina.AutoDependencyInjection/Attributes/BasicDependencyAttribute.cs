using TakasakiStudio.Lina.AutoDependencyInjection.Interfaces;

namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Base attribute for implement other attributes 
/// </summary>
public abstract class BasicDependencyAttribute<T> : Attribute, IDynamicServicesProviderAttribute
{
    public abstract LifeTime LifeTime { get; }
    public Type Interface { get; }
    public DependencyType DependencyType { get; }

    protected BasicDependencyAttribute(DependencyType dependencyType = DependencyType.Normal)
    {
        DependencyType = dependencyType;
        Interface = typeof(T);
    } 
}