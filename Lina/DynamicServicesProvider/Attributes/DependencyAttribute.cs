using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute : Attribute, IDynamicServicesProviderAttribute
{
    public LifeTime LifeTime { get; }
    public Type? Interface { get; }
    
    public DependencyAttribute(LifeTime lifeTime, Type? @interface = null)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}