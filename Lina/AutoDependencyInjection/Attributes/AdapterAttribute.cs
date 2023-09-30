namespace Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Defines a class as adapter
/// <example>
/// <code>
/// interface IFooAdapter {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Adapter(typeof(IFooAdapter))]
/// class FooAdapter : IFooAdapter {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AdapterAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }

    public AdapterAttribute(Type @interface,  LifeTime lifeTime = LifeTime.Singleton)
    {
        Interface = @interface;
        LifeTime = lifeTime;
    }
}