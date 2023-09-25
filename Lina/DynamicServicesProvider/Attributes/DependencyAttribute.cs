using Lina.DynamicServicesProvider.Interfaces;

namespace Lina.DynamicServicesProvider.Attributes;

/// <summary>
/// Defines a class as dependency in dependency injection and define life time 
/// <example>
/// <code>
/// interface IFooDependency {
///     /*...*/
/// }
/// </code>
/// <code>
/// [Dependency(typeof(IFooDependency), LifeTime.Scoped)]
/// class FooDependency : IFooDependency {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DependencyAttribute : BasicDependencyAttribute
{
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
    
    public DependencyAttribute(LifeTime lifeTime, Type? @interface = null)
    {
        LifeTime = lifeTime;
        Interface = @interface;
    }
}