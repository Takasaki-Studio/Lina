namespace Lina.DynamicServicesProvider.Attributes;

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