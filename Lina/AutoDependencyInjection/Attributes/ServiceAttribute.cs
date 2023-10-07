namespace Lina.AutoDependencyInjection.Attributes;

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
public class ServiceAttribute<T>(LifeTime lifeTime = LifeTime.Scoped) : BasicDependencyAttribute<T>
{
    public override LifeTime LifeTime { get; } = lifeTime;
}