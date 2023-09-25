using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

/// <summary>
/// Defines a class as service
/// <example>
/// <code>
/// interface IFooService {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Service(typeof(IFooService))]
/// class FooService : IFooService {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
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