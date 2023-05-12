using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
    
    public ServiceAttribute(Type @interface, LifeTime lifeTime = LifeTime.Scoped)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}