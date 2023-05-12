using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }

    public AdapterAttribute(Type @interface,  LifeTime lifeTime = LifeTime.Singleton)
    {
        Interface = @interface;
        LifeTime = lifeTime;
    }
}