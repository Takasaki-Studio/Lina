using Lina.DynamicServicesProvider.Attributes;
using Lina.RepositoryFactory.Database;
using Lina.RepositoryFactory.Repository;
using Lina.RepositoryFactory.Repository.Interfaces;
using Lina.Test.RepositoryFactory.Models;
using Lina.Test.RepositoryFactory.Repositories.Interfaces;

namespace Lina.Test.RepositoryFactory.Repositories;

[Repository(typeof(IUserRepository))]
public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    public UserRepository(LinaDbContext context) : base(context)
    {
    }
}