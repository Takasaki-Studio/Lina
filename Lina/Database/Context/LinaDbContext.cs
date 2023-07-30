using Lina.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lina.Database.Context;

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