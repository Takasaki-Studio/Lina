using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

/// <summary>
/// Defines a class as repository
/// <example>
/// <code>
/// interface IFooRepository {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Repository(typeof(IFooRepository))]
/// class FooRepository : IFooRepository {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class RepositoryAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
    
    public RepositoryAttribute(Type @interface, LifeTime lifeTime = LifeTime.Scoped)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}