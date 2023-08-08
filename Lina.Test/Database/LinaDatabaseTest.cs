using Lina.Database;
using Lina.Database.Context;
using Lina.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Test.Database;

[TestClass]
public class LinaDatabaseTest
{
    [TestMethod]
    public void AddLinaDbContext_ChecksIfTheDbContextAndTheAssemblyWereRegisteredCorrectly()
    {
        var servicesCollection = new ServiceCollection();
        
        servicesCollection.AddLinaDbContext<LinaDatabaseTest>((_) => {});

        var provider = servicesCollection.BuildServiceProvider();

        var assemblyConfig = provider.GetRequiredService<IAssemblyEntityConfigurationInjection>();
        Assert.IsNotNull(assemblyConfig?.ConfigurationAssembly);

        var dbContext = provider.GetRequiredService<DbContext>();
        Assert.IsNotNull(dbContext);
        Assert.IsInstanceOfType<LinaDbContext>(dbContext);
    }
    
    [TestMethod]
    public void AddLinaDbContext_checksIfAssemblyIsReturned()
    {
        var servicesCollection = new ServiceCollection();
        
        servicesCollection.AddLinaDbContext<LinaDatabaseTest>((_, assembly) =>
        {
            Assert.AreEqual(typeof(LinaDatabaseTest).Assembly.FullName, assembly);
        });

        var provider = servicesCollection.BuildServiceProvider();

        var assemblyConfig = provider.GetRequiredService<IAssemblyEntityConfigurationInjection>();
        Assert.IsNotNull(assemblyConfig?.ConfigurationAssembly);

        var dbContext = provider.GetRequiredService<DbContext>();
        Assert.IsNotNull(dbContext);
        Assert.IsInstanceOfType<LinaDbContext>(dbContext);
    }
}