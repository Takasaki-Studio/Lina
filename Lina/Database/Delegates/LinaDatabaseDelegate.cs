using Microsoft.EntityFrameworkCore;

namespace Lina.Database.Delegates;

/// <summary>
/// Delegate with database configuration and assembly name
/// </summary>
public delegate void LinaDbContextConfigurationAssembly(DbContextOptionsBuilder optionsBuilder, string? assembly);

/// <summary>
/// Delegate with database configuration 
/// </summary>
public delegate void LinaDbContextConfiguration(DbContextOptionsBuilder optionsBuilder);