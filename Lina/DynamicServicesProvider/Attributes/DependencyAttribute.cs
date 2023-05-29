using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
    
    public DependencyAttribute(LifeTime lifeTime, Type? @interface = null)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}