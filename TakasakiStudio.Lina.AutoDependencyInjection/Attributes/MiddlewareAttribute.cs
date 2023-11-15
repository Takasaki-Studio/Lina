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
/// [Middleware(typeof(IFooMiddleware))]
/// class FooMiddleware : IFooMiddleware {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class MiddlewareAttribute<T>(LifeTime lifeTime = LifeTime.Transient) : BasicDependencyAttribute<T>
{
    public override LifeTime LifeTime { get; } = lifeTime;
}