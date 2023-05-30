using Lina.DynamicServicesProvider;
using Lina.RepositoryFactory;
using Lina.RepositoryFactory.Repository.Interfaces;
using Lina.Test.RepositoryFactory.Models;
using Lina.Test.RepositoryFactory.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.Test.RepositoryFactory;

[TestClass]
public class RepositoryFactoryTest
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactoryTest()
    {
        var builder = new ServiceCollection();
        builder.AddDynamicServices<RepositoryFactoryTest>();
        builder.AddUnitOfWork();
        _serviceProvider = builder.BuildServiceProvider();
    }

    [TestMethod]
    public void TestLoadUnitOfWork()
    {
        var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        Assert.IsNotNull(unitOfWork);
    }

    [TestMethod]
    public void TestLoadRepository()
    {
        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        Assert.IsNotNull(userRepository);
    }

    [TestMethod]
    public async Task TestAdd()
    {
        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        var model = new UserModel("vulcan");
        
        await userRepository.Add(model);
        await unitOfWork.SaveChanges();
        
        Assert.AreEqual(model.Id, 1);
    }

    [TestMethod]
    public async Task TestUpdate()
    {
        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        var model = new UserModel("vulcan");
        
        await userRepository.Add(model);
        await unitOfWork.SaveChanges();
        
        unitOfWork.ClearContext();

        model.Name = "takasaki";
        
        userRepository.Update(model);
        await unitOfWork.SaveChanges();

        var user = await userRepository.GetById(model.Id);
        
        Assert.AreEqual(user?.Name, "takasaki");
    }

    [TestMethod]
    public async Task TestDelete()
    {
        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        var model = new UserModel("vulcan");
        
        await userRepository.Add(model);
        await unitOfWork.SaveChanges();
        
        unitOfWork.ClearContext();
        
        userRepository.Delete(model);
        await unitOfWork.SaveChanges();

        var user = await userRepository.GetById(model.Id);
        
        Assert.IsNull(user);
    }

    [TestMethod]
    public async Task TestGet()
    {
        var userRepository = _serviceProvider.GetRequiredService<IUserRepository>();
        var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        var model = new UserModel("vulcan");
        
        await userRepository.Add(model);
        await unitOfWork.SaveChanges();
        
        var user = await userRepository.GetById(1);
        
        Assert.IsNotNull(user);
    }
}