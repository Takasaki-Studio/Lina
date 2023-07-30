using Lina.Database.Context;
using Lina.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Database;

public static class LinaDatabase
{
    public static void AddLinaDbContext<T>(
        this IServiceCollection service,
        Action<DbContextOptionsBuilder>? dbContextOptions = null,
        ServiceLifetime dbContextLifetime = ServiceLifetime.Scoped)
    {
        var tAssembly = typeof(T).Assembly;
        var assemblyEntityConfigurationInject = new AssemblyEntityConfigurationInjection(tAssembly);
        service.AddSingleton<IAssemblyEntityConfigurationInjection>(assemblyEntityConfigurationInject);
        
        service.AddDbContext<DbContext, LinaDbContext>(dbContextOptions, dbContextLifetime);
    }
}