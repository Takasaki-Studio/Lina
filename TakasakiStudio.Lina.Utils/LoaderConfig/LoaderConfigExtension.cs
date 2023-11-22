using Config.Net;
using Microsoft.Extensions.DependencyInjection;

namespace TakasakiStudio.Lina.Utils.LoaderConfig;

/// <summary>
/// Auto load configs from json file or env variables
/// </summary>
public static class LoaderConfigExtension
{
    /// <summary>
    /// Load config from json file or env variable using <a href="https://github.com/aloneguid/config">Config.Net</a> and inject in dependency injection
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="jsonFileName">Json file name</param>
    /// <typeparam name="T">Interface with settings</typeparam>
    /// <returns>Interface with loaded configurations</returns>
    public static T AddLoaderConfig<T>(this IServiceCollection serviceCollection, string jsonFileName = "config.json")
        where T : class
    {
        var config = new ConfigurationBuilder<T>()
            .UseEnvironmentVariables()
            .UseJsonFile(jsonFileName)
            .Build();

        serviceCollection.AddSingleton(config);
        return config;
    }
}