using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TakasakiStudio.Lina.Database.Context;
using TakasakiStudio.Lina.Database.Delegates;
using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database;

/// <summary>
/// Auxiliary functions for service builder
/// </summary>
public static class LinaDatabase
{
    /// <summary>
    /// Inject <see cref="LinaDbContext"/> into dependency injection
    /// </summary>
    /// <param name="service">Service collection</param>
    /// <param name="dbContextOptions">Database configuration with assembly name for migration</param>
    /// <param name="dbContextLifetime">Context life time</param>
    /// <typeparam name="T">Class for assembly reference</typeparam>
    /// <example>
    /// <code>
    /// serviceCollection.AddLinaDbContext&lt;Program&gt;((builder, assembly) => builder.UseMySql("url",
    ///     ServerVersion.AutoDetect("url"), options => options.MigrationsAssembly(assembly)));
    /// </code>
    /// </example>
    public static void AddLinaDbContext<T>(
        this IServiceCollection service,
        LinaDbContextConfigurationAssembly dbContextOptions,
        ServiceLifetime dbContextLifetime = ServiceLifetime.Scoped)
    {
        var tAssembly = typeof(T).Assembly;
        var assemblyEntityConfigurationInject = new AssemblyEntityConfigurationInjection(tAssembly);
        service.AddSingleton<IAssemblyEntityConfigurationInjection>(assemblyEntityConfigurationInject);

        service.AddDbContext<DbContext, LinaDbContext>(x => dbContextOptions.Invoke(x, tAssembly.FullName),
            dbContextLifetime);
    }

    /// <summary>
    /// Inject <see cref="LinaDbContext"/> into dependency injection
    /// </summary>
    /// <param name="service">Service collection</param>
    /// <param name="dbContextOptions">Database configuration</param>
    /// <param name="dbContextLifetime">Context life time</param>
    /// <typeparam name="T">Class for assembly reference</typeparam>
    /// <example>
    /// <code>
    /// serviceCollection.AddLinaDbContext&lt;Program&gt;((builder) => builder.UseMySql("url",
    ///     ServerVersion.AutoDetect("url")));
    /// </code>
    /// </example>
    public static void AddLinaDbContext<T>(this IServiceCollection service, LinaDbContextConfiguration dbContextOptions,
        ServiceLifetime dbContextLifetime = ServiceLifetime.Scoped)
    {
        service.AddLinaDbContext<T>((options, _) => { dbContextOptions.Invoke(options); }, dbContextLifetime);
    }
}