using TakasakiStudio.Lina.Database.Interfaces;
using TakasakiStudio.Lina.Services.Interfaces;

namespace TakasakiStudio.Lina.Services;

public class BaseService<TEntity, TPkType>(IBaseRepository<TEntity, TPkType> baseRepository) : IBaseService
    where TEntity : class, IBaseEntity<TPkType>
{
}