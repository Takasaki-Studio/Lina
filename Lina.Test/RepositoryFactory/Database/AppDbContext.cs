using Lina.DynamicServicesProvider.Attributes;
using Lina.RepositoryFactory.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lina.Test.RepositoryFactory.Database;

[Database]
public class AppDbContext : LinaDbContext
{
    public AppDbContext(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("test");
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}