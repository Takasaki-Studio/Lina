using TakasakiStudio.Lina.Database.Interfaces;
using TakasakiStudio.Lina.Services.Interfaces;

namespace TakasakiStudio.Lina.Services;

public class BaseService<TEntity, TPkType> : IBaseService
    where TEntity : class, IBaseEntity<TPkType>
{
}