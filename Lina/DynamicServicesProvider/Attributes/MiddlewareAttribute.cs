using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class MiddlewareAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
    
    public MiddlewareAttribute(LifeTime lifeTime = LifeTime.Transient)
    {
        LifeTime = lifeTime;
        Interface = null;
    }
}