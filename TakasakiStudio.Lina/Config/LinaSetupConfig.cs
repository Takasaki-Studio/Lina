using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.Database;
using TakasakiStudio.Lina.Exceptions;
using TakasakiStudio.Lina.Utils.LoaderConfig;

namespace TakasakiStudio.Lina.Config;

public class LinaSetupConfig<TAppConfig>(IServiceCollection collection)
    where TAppConfig : class
{
    private TAppConfig? _config;

    public LinaSetupConfig<TAppConfig> EnableDependencyInjection()
    {
        collection.AddAutoDependencyInjection<TAppConfig>();
        return this;
    }

    public LinaSetupConfig<TAppConfig> EnableConfig()
    {
        _config = collection.AddLoaderConfig<TAppConfig>();
        return this;
    }

    public LinaSetupConfig<TAppConfig> EnableDatabase(DatabaseConfigDelegate config)
    {
        if (_config is null)
        {
            throw new LinaException("Config is not enabled");
        }

        collection.AddLinaDbContext<TAppConfig>((dbConfig, migration) => config(dbConfig, migration, _config));
        return this;
    }


    public delegate void DatabaseConfigDelegate(DbContextOptionsBuilder dbConfig, string? migrationAssembly,
        TAppConfig config);
}