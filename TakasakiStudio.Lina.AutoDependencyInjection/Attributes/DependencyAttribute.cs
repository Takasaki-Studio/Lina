namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Defines a class as dependency in dependency injection and define life time 
/// <example>
/// <code>
/// interface IFooDependency {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Dependency&lt;IFooDependency&gt;(LifeTime.Scoped)]
/// class FooDependency : IFooDependency {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute<T>(LifeTime lifeTime) : BasicDependencyAttribute<T>
{
    public override LifeTime LifeTime { get; } = lifeTime;
}