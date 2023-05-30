using Lina.RepositoryFactory.Database;
using Lina.RepositoryFactory.Repository.Interfaces;

namespace Lina.RepositoryFactory.Repository;

public class BaseRepository<TModel, TKey> : IBaseRepository<TModel, TKey> where TModel : class
{
    private readonly LinaDbContext _context;

    protected BaseRepository(LinaDbContext context)
    {
        _context = context;
    }

    public virtual async ValueTask Add(TModel model)
    {
        await _context.Set<TModel>().AddAsync(model);
    }

    public virtual async Task<TModel?> GetById(TKey id)
    {
        return await _context.Set<TModel>().FindAsync(id);
    }

    public virtual void Update(TModel model)
    {
        _context.Set<TModel>().Update(model);
    }

    public virtual void Delete(TModel model)
    {
        _context.Set<TModel>().Remove(model);
    }
}

public class BaseRepository<TModel> : BaseRepository<TModel, int> where TModel : class
{
    public BaseRepository(LinaDbContext context) : base(context)
    {
    }
}