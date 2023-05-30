using Config.Net;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.LoaderConfig;

public static class LoaderConfigExtension
{
    public static T AddLoaderConfig<T>(this IServiceCollection serviceCollection) where T : class
    {
        var config = new ConfigurationBuilder<T>()
            .UseJsonFile("config.json")
            .UseEnvironmentVariables()
            .Build();

        serviceCollection.AddSingleton(config);
        return config;
    }
}