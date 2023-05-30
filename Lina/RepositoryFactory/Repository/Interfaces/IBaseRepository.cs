namespace Lina.RepositoryFactory.Repository.Interfaces;

public interface IBaseRepository<TModel, in TKey>
{
    ValueTask Add(TModel model);
    Task<TModel?> GetById(TKey id);
    void Update(TModel model);
    void Delete(TModel model);
}

public interface IBaseRepository<TModel> : IBaseRepository<TModel, int>
{
}