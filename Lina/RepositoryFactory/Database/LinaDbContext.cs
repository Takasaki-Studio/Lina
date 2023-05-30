using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lina.RepositoryFactory.Database;

public class LinaDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    
    public LinaDbContext(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseLoggerFactory(_loggerFactory);
    }
}