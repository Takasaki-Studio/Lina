using Lina.AutoDependencyInjection;
using Lina.AutoDependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Test.DynamicServicesProvider;

[TestClass]
public class ServiceCollectionExtensionTest
{
    private readonly IServiceProvider _serviceProvider;
    
    public ServiceCollectionExtensionTest()
    {
        var builder = new ServiceCollection();
        builder.AddAutoDependencyInjection<ServiceCollectionExtensionTest>();
        _serviceProvider = builder.BuildServiceProvider();
    }
    
    [TestMethod]
    public void TestLoadIService()
    {
        var serviceLoaded = _serviceProvider.GetRequiredService<IService>();
        Assert.IsNotNull(serviceLoaded);
    }

    [TestMethod]
    public void TestLoadHttpClientService()
    {
        var serviceLoaded = _serviceProvider.GetRequiredService<IService>();
        Assert.IsTrue(serviceLoaded.HasClient);
    }
}


public interface IService
{
    bool HasClient { get; }
}

[Service(typeof(IService))]
public class Service : IService
{
    private readonly IHttpClientService? _httpClient;

    public Service(IHttpClientService? httpClient)
    {
        _httpClient = httpClient;
    }

    public bool HasClient => _httpClient is not null;
}

public interface IHttpClientService
{
    public HttpClient HttpClient { get; }
}

[HttpClient(typeof(IHttpClientService))]
public class HttpClientService : IHttpClientService
{
    public HttpClient HttpClient { get; }

    public HttpClientService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}