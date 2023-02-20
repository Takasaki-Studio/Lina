using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class MiddlewareAttribute : Attribute, IDynamicServicesProviderAttribute
{
    public LifeTime LifeTime { get; }
    public Type? Interface { get; }
    
    public MiddlewareAttribute(LifeTime lifeTime = LifeTime.Transient)
    {
        LifeTime = lifeTime;
        Interface = null;
    }
}