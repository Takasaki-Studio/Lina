namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Defines a class as middleware
/// <example>
/// <code>
/// interface IFooMiddleware {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Middleware&lt;IFooMiddleware&gt;]
/// class FooMiddleware : IFooMiddleware {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class MiddlewareAttribute(LifeTime lifeTime = LifeTime.Transient) : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; } = lifeTime;
    public override Type? Interface => null;
}