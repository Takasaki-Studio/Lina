namespace Lina.DynamicServicesProvider.Interfaces;

public interface IDynamicServicesProviderAttribute
{
    LifeTime LifeTime { get; }
    Type? Interface { get; }
    DependencyType DependencyType { get; }
}