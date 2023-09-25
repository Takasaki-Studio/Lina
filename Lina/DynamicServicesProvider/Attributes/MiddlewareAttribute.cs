using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

/// <summary>
/// Defines a class as middleware
/// <example>
/// <code>
/// interface IFooMiddleware {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Middleware(typeof(IFooMiddleware))]
/// class FooMiddleware : IFooMiddleware {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
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