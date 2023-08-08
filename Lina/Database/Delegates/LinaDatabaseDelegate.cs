using Microsoft.EntityFrameworkCore;

namespace Lina.Database.Delegates;

public delegate void LinaDbContextConfigurationAssembly(DbContextOptionsBuilder optionsBuilder, string? assembly);
public delegate void LinaDbContextConfiguration(DbContextOptionsBuilder optionsBuilder);