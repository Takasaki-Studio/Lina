namespace TakasakiStudio.Lina.AutoDependencyInjection.Attributes;

/// <summary>
/// Defines a class as http client
/// <example>
/// <code>
/// interface IFooHttpClient {
///     /*...*/
/// }
/// </code>
/// <code>
/// [HttpClient(typeof(IFooHttpClient))]
/// class FooHttpClient : IFooHttpClient {
///     /*...*/
/// }
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class HttpClientAttribute<T>() : BasicDependencyAttribute<T>(DependencyType.Http)
{
    public override LifeTime LifeTime { get; } = LifeTime.Transient;
}