namespace Lina.DynamicServicesProvider.Attributes;

public class DatabaseAttribute : BasicDependencyAttribute
{
    public DatabaseAttribute() : base(DependencyType.Database)
    {
        LifeTime = LifeTime.Scoped;
        Interface = null;
    }
    
    public override LifeTime LifeTime { get; }
    public override Type? Interface { get; }
}