namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Defines a class as adapter
/// <example>
/// <code>
/// interface IFooAdapter {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Adapter&lt;IFooAdapter&gt;]
/// class FooAdapter : IFooAdapter {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute<T>(LifeTime lifeTime = LifeTime.Singleton) : BasicDependencyAttribute<T>
{
    public override LifeTime LifeTime { get; } = lifeTime;
}