namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Defines a class as service
/// <example>
/// <code>
/// interface IFooService {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Service&lt;IFooService&gt;]
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