namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

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
public class RepositoryAttribute<T>(LifeTime lifeTime = LifeTime.Scoped) : BasicDependencyAttribute<T>
{
    public override LifeTime LifeTime { get; } = lifeTime;
}