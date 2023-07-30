using Lina.Database;
using Lina.Database.Context;
using Lina.Database.Interfaces;
using Lina.LoaderConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Test.Database;

[TestClass]
public class LinaDatabaseTest
{
    [TestMethod]
    public void ChecksIfTheDbContextAndTheAssemblyWereRegisteredCorrectly()
    {
        var servicesCollection = new ServiceCollection();
        
        servicesCollection.AddLinaDbContext<LinaDatabaseTest>();

        var provider = servicesCollection.BuildServiceProvider();

        var assemblyConfig = provider.GetRequiredService<IAssemblyEntityConfigurationInjection>();
        Assert.IsNotNull(assemblyConfig?.ConfigurationAssembly);

        var dbContext = provider.GetRequiredService<DbContext>();
        Assert.IsNotNull(dbContext);
        Assert.IsInstanceOfType<LinaDbContext>(dbContext);
    }
}