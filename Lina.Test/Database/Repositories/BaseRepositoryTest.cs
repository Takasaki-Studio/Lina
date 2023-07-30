using Lina.Database;
using Lina.Database.Context;
using Lina.Database.Interfaces;
using Lina.Database.Models;
using Lina.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace Lina.Test.Database.Repositories;

[TestClass]
public class BaseRepositoryTest
{
    [TestCleanup]
    public async Task CleanDb()
    {
        var repo = CreateTestRepository();
        await repo.CleanDb();
    }
    
    [TestMethod]
    public async Task GetById_ChecksIfEntityId32IsFetched()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 32
        });
        
        await repo.Add(new TestEntity
        {
            Id = 3
        });

        var result = await repo.GetById(32);
        Assert.AreEqual(32, result?.Id);
    }

    [TestMethod]
    public async Task Get_ChecksIfEntityWithId34IsReturned()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 34
        });

        await repo.Add(new TestEntity
        {
            Id = 5
        });

        await repo.Commit();

        var result = await repo.Get(x => x.Id == 34);
        
        Assert.AreEqual(34, result?.Id);
    }

    [TestMethod]
    public async Task GetAll_ChecksIfAllEvenRecordsAreReturnedInTheList()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 34
        });

        await repo.Add(new TestEntity
        {
            Id = 5
        });

        await repo.Add(new TestEntity
        {
            Id = 32
        });

        await repo.Add(new TestEntity
        {
            Id = 7
        });

        await repo.Commit();

        var results = (await repo.GetAll(x => x.Id % 2 == 0)).ToList();
        
        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results.All(x => x.Id % 2 == 0));
    }

    [TestMethod]
    public async Task Exists_ChecksIfItSaysThatEntityWithValue2IsPresent()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 37
        });

        await repo.Add(new TestEntity
        {
            Id = 2
        });
        await repo.Commit();

        var result = await repo.Exists(x => x.Id == 2);
        
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public async Task Exists_ChecksIfItSaysThatEntityWithValue2IsNotPresent()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 48
        });

        await repo.Add(new TestEntity
        {
            Id = 3
        });
        await repo.Commit();

        var result = await repo.Exists(x => x.Id == 2);
        
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task Add_ChecksIfTheInsertionWasSuccessful()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 1
        });
        await repo.Commit();
    }

    [TestMethod]
    public async Task Update_TestEntityIsUpdated()
    {
        var repo = CreateTestRepository();
        await repo.Add(new TestEntity
        {
            Id = 23,
            Test = "t1"
        });
        await repo.Commit();

        {
            var result1 = await repo.GetById(23);
            Assert.AreEqual("t1", result1?.Test);

            if (result1 is null)
                throw new Exception("Result 1 is null");
        
            result1.Test = "t2";
            repo.Update(result1);
            await repo.Commit();
            repo.Detach(result1);
        }

        var result2 = await repo.GetById(23);
        Assert.AreEqual("t2", result2?.Test);
    }

    [TestMethod]
    public async Task Delete_ChecksIfTheElementIsRemovedFromTheDatabase()
    {
        var repo = CreateTestRepository();
        {
            await repo.Add(new TestEntity
            {
                Id = 5
            });

            await repo.Add(new TestEntity
            {
                Id = 7
            });

            var count = await repo.Commit();
            Assert.AreEqual(2, count);
            
            repo.DetachAllEntities();
        
            repo.Delete(new TestEntity
            {
                Id = 7
            });
            await repo.Commit();
            repo.DetachAllEntities();
        }
        
        var it5 = await repo.GetById(5);
        var it7 = await repo.GetById(7);
        
        Assert.AreEqual(5, it5?.Id);
        Assert.IsNull(it7);
    }

    private static TestRepository CreateTestRepository()
    {
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase("test");
        var assembly = new AssemblyEntityConfigurationInjection(typeof(BaseRepositoryTest).Assembly);
        
        var linaDbContextExample = new LinaDbContext(options.Options, assembly, Array.Empty<ILoggerFactory>());
        return new TestRepository(linaDbContextExample);
    }
}

public class TestEntity : BaseEntity<int>
{
    public string? Test { get; set; }
}

internal class TestRepository : BaseRepository<TestEntity, int>
{
    private readonly DbContext _context;
    
    public TestRepository(DbContext context) : base(context)
    {
        _context = context;
    }

    public async ValueTask CleanDb()
    {
        await _context.Database.EnsureDeletedAsync();
    }
}

public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
    }
}