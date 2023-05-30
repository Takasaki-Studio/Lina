using Config.Net;
using Lina.LoaderConfig;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Test.LoaderConfig;

public interface IConfig
{
    [Option(DefaultValue = "Test")]
    public string Test { get; set; }
}

[TestClass]
public class LoaderConfigExtensionTest
{
    private readonly IServiceProvider _serviceProvider;

    public LoaderConfigExtensionTest()
    {
        var builder = new ServiceCollection();
        builder.AddLoaderConfig<IConfig>();
        _serviceProvider = builder.BuildServiceProvider();
    }

    [TestMethod]
    public void TestLoadConfig()
    {
        var config = _serviceProvider.GetRequiredService<IConfig>();
        Assert.AreEqual(config.Test, "Test");
    }

    [TestMethod]
    public void TestConfigReturn()
    {
        var builder = new ServiceCollection();
        var config = builder.AddLoaderConfig<IConfig>();
        Assert.AreEqual(config.Test, "Test");
    }
}