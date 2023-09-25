namespace Lina.DynamicServicesProvider.Attributes;

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
public class HttpClientAttribute : BasicDependencyAttribute
{
    public HttpClientAttribute(Type @interface) : base(DependencyType.Http)
    {
        LifeTime = LifeTime.Transient;
        Interface = @interface;
    }

    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
}