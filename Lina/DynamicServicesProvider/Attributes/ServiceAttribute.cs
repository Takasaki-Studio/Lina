using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute : Attribute, IDynamicServicesProviderAttribute
{
    public LifeTime LifeTime { get; }
    public Type? Interface { get; }
    
    public ServiceAttribute(Type @interface, LifeTime lifeTime = LifeTime.Scoped)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}