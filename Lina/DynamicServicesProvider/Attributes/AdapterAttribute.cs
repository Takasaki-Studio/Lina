using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute : Attribute, IDynamicServicesProviderAttribute
{
    public LifeTime LifeTime { get; }
    public Type? Interface { get; }

    public AdapterAttribute(Type @interface,  LifeTime lifeTime = LifeTime.Singleton)
    {
        Interface = @interface;
        LifeTime = lifeTime;
    }
}