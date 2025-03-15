using Microsoft.Extensions.DependencyInjection;
using TakasakiStudio.Lina.Config;
using TakasakiStudio.Lina.Services;
using TakasakiStudio.Lina.Services.Interfaces;

namespace TakasakiStudio.Lina;

public static class LinaSetup
{
    public static void AddLina<TAppConfig>(this IServiceCollection collection, 
        Action<LinaSetupConfig<TAppConfig>> config)
        where TAppConfig : class
    {
        var builder = new LinaSetupConfig<TAppConfig>(collection);
        config(builder);

        collection.AddScoped<IBaseService, BaseService>();
    }
}