using Lina.RepositoryFactory.Database;
using Lina.RepositoryFactory.Exceptions;
using Lina.RepositoryFactory.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lina.RepositoryFactory.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly LinaDbContext _context;

    public UnitOfWork(LinaDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveChanges()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DuplicateException(ex.Message);
        }
    }
    
    public Task<IDbContextTransaction> Begin()
    {
        return _context.Database.BeginTransactionAsync();
    }

    public void ClearContext()
    {
        _context.ChangeTracker.Clear();
    }
}