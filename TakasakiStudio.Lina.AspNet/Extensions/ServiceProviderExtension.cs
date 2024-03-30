using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using TakasakiStudio.Lina.AspNet.Providers;

namespace TakasakiStudio.Lina.AspNet.Extensions;

public static class ServiceProviderExtension
{
    public static void AddFileVersionProvider(this IServiceCollection services)
    {
        services.AddSingleton<IFileVersionProvider, FileVersionProvider>();
    }
}