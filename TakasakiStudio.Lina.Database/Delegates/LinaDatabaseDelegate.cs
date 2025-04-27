using Microsoft.EntityFrameworkCore;

namespace TakasakiStudio.Lina.Database.Delegates;

/// <summary>
/// Delegate with database configuration and assembly name
/// </summary>
public delegate void LinaDbContextConfigurationAssembly(DbContextOptionsBuilder optionsBuilder, string? assembly);

/// <summary>
/// Delegate with database configuration 
/// </summary>
public delegate void LinaDbContextConfiguration(DbContextOptionsBuilder optionsBuilder);

/// <summary>
/// Delegate with database configuration and assembly name
/// </summary>
public delegate void LinaDbContextConfigurationAssemblyService(IServiceProvider serviceProvider,
    DbContextOptionsBuilder optionsBuilder, string? assembly);

/// <summary>
/// Delegate with database configuration 
/// </summary>
public delegate void LinaDbContextConfigurationService(IServiceProvider serviceProvider,
    DbContextOptionsBuilder optionsBuilder);