using System.Reflection;
using Lina.DynamicServicesProvider.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.DynamicServicesProvider;

public static class ServiceCollectionExtension
{
    public static void AddDynamicServices<T>(this IServiceCollection services)
    {
        var assembly = typeof(T).Assembly;
        AddServicesFromAssembly(assembly, services);
    }

    private static void AddServicesFromAssembly(Assembly assembly, IServiceCollection services)
    {
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            var attributes = Attribute.GetCustomAttributes(type);
            foreach (var attribute in attributes)
            {
                if(attribute is not IDynamicServicesProviderAttribute diDynamicAttribute) continue;
                switch (diDynamicAttribute.LifeTime)
                {
                    case LifeTime.Scoped:
                        if (diDynamicAttribute.Interface != null)
                            services.AddScoped(diDynamicAttribute.Interface, type);
                        else
                            services.AddScoped(type);
                        break;
                    case LifeTime.Singleton:
                        if(diDynamicAttribute.Interface != null)
                            services.AddSingleton(diDynamicAttribute.Interface, type);
                        else
                            services.AddSingleton(type);
                        break;
                    case LifeTime.Transient:
                        if(diDynamicAttribute.Interface != null)
                            services.AddTransient(diDynamicAttribute.Interface, type);
                        else
                            services.AddTransient(type);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(diDynamicAttribute.LifeTime), "LifeTime not supported");
                }
            }
        }
    }
}