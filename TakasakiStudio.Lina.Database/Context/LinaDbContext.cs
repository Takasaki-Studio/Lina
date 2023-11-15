using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database.Context;

/// <summary>
/// Implementation of <see cref="DbContext"/> with base configuration for dependency injection
/// </summary>
public class LinaDbContext : DbContext
{
    private readonly IAssemblyEntityConfigurationInjection? _assemblyEntityConfigurationInjection;
    private readonly ILoggerFactory? _loggerFactory;

    public LinaDbContext(
        DbContextOptions options,
        IAssemblyEntityConfigurationInjection? assemblyEntityConfigurationInjection, 
        IEnumerable<ILoggerFactory> loggerFactories) : base(options)
    {
        _assemblyEntityConfigurationInjection = assemblyEntityConfigurationInjection;
        _loggerFactory = loggerFactories.FirstOrDefault();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        if (_loggerFactory is not null)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if(_assemblyEntityConfigurationInjection is null) return;
        modelBuilder.ApplyConfigurationsFromAssembly(_assemblyEntityConfigurationInjection.ConfigurationAssembly);
    }
}