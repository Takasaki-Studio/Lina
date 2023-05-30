using Lina.RepositoryFactory.Repository;
using Lina.RepositoryFactory.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.RepositoryFactory;

public static class RepositoryFactoryExtension
{
    public static void AddUnitOfWork(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}