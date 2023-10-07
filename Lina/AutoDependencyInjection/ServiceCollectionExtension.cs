using System.Reflection;
using Lina.AutoDependencyInjection.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.AutoDependencyInjection;

/// <summary>
/// Auxiliary functions for service builder
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    ///  Auto inject class with attributes
    ///  [<see cref="services"/>]
    ///  [<see cref="DependencyAttribute{T}"/>]
    ///  [<see cref="AdapterAttribute"/>]
    ///  [<see cref="DependencyAttribute"/>]
    ///  [<see cref="HttpClientAttribute"/>]
    ///  [<see cref="MiddlewareAttribute"/>]
    ///  into dependency injection
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <typeparam name="T">Class for assembly reference</typeparam>
    public static void AddAutoDependencyInjection<T>(this IServiceCollection services)
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
                if (attribute is not IDynamicServicesProviderAttribute diDynamicAttribute) continue;

                switch (diDynamicAttribute.DependencyType)
                {
                    case DependencyType.Http:
                    {
                        var extensionClass = typeof(HttpClientFactoryServiceCollectionExtensions);
                        var extensionMethod = extensionClass.GetMethod("AddHttpClient", 2,
                            new[] { typeof(IServiceCollection) });
                        if (extensionMethod is null || diDynamicAttribute.Interface is null) continue;

                        var genericMethod = extensionMethod.MakeGenericMethod(diDynamicAttribute.Interface, type);
                        genericMethod.Invoke(null, new object[] { services });
                        continue;
                    }
                    default:
                        switch (diDynamicAttribute.LifeTime)
                        {
                            case LifeTime.Scoped:
                                if (diDynamicAttribute.Interface != null)
                                    services.AddScoped(diDynamicAttribute.Interface, type);
                                else
                                    services.AddScoped(type);
                                break;
                            case LifeTime.Singleton:
                                if (diDynamicAttribute.Interface != null)
                                    services.AddSingleton(diDynamicAttribute.Interface, type);
                                else
                                    services.AddSingleton(type);
                                break;
                            case LifeTime.Transient:
                                if (diDynamicAttribute.Interface != null)
                                    services.AddTransient(diDynamicAttribute.Interface, type);
                                else
                                    services.AddTransient(type);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(diDynamicAttribute.LifeTime),
                                    "LifeTime not supported");
                        }

                        break;
                }
            }
        }
    }
}