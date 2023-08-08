using Lina.Database.Context;
using Lina.Database.Delegates;
using Lina.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Database;

public static class LinaDatabase
{
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

    public static void AddLinaDbContext<T>(this IServiceCollection service, LinaDbContextConfiguration dbContextOptions,
        ServiceLifetime dbContextLifetime = ServiceLifetime.Scoped)
    {
        service.AddLinaDbContext<T>((options, _) => { dbContextOptions.Invoke(options); }, dbContextLifetime);
    }
}