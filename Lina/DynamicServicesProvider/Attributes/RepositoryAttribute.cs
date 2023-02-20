using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RepositoryAttribute : Attribute, IDynamicServicesProviderAttribute
{
    public LifeTime LifeTime { get; }
    public Type? Interface { get; }
    
    public RepositoryAttribute(Type @interface, LifeTime lifeTime = LifeTime.Scoped)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}