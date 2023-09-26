# Lina

A framework to simplify application creation by improving dependency injection, validation, config and database handling

## Features

- Config
  - Easy load
  - Auto inject
- Database 
  - Auto inject 
  - Auto import configuration
  - Easy configuration
- Validation
  - Fluent api
  - Reliable library
  - Easy usage
- Dependency Injection
  - Life time configurable
  - Easy manipulation
  - Services, Repositories, HttpClient and more types of preconfigured dependencies

## Example simple usage

```csharp
using Config.Net;
using Lina.DynamicServicesProvider;
using Lina.DynamicServicesProvider.Attributes;
using Lina.LoaderConfig;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddLoaderConfig<IAppConfig>();
serviceCollection.AddDynamicServices<Program>();

var services = serviceCollection.BuildServiceProvider();

services.GetRequiredService<IFooService>().PrintAppName();

public interface IFooService 
{
    public void PrintAppName();
}

[Service(typeof(IFooService))]
public class FooService : IFooService
{
    private readonly IAppConfig _appConfig;
    
    public FooService(IAppConfig appConfig)
    {
        _appConfig = appConfig;
    }
    
    public void PrintAppName()
    {
        Console.WriteLine(_appConfig.AppName);
    }
}

public interface IAppConfig 
{
    [Option(DefaultValue = "Test")]
    public string AppName { get; }
}
```

## Config example usage

```csharp
using Config.Net;
using Lina.LoaderConfig;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

var config = serviceCollection.AddLoaderConfig<IAppConfig>();

Console.WriteLine(config.AppName); // instant use

var services = serviceCollection.BuildServiceProvider();

Console.WriteLine(services.GetRequiredService<IAppConfig>().AppName); // DI usage

public interface IAppConfig 
{
    [Option(DefaultValue = "Test")]
    public string AppName { get; }
}
```

## Database example usage

```csharp
using Config.Net;
using Lina.Database;
using Lina.Database.Models;
using Lina.LoaderConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

var config = serviceCollection.AddLoaderConfig<IAppConfig>();
serviceCollection.AddLinaDbContext<Program>((builder, assembly) =>
    builder.UseMySql(config.DatabaseUrl, ServerVersion.AutoDetect(config.DatabaseUrl),
        optionsBuilder => optionsBuilder.MigrationsAssembly(assembly)));

public interface IAppConfig 
{
    [Option(DefaultValue = "Server=localhost;Database=test;User Id=root;Password=root;")]
    public string DatabaseUrl { get; }
}

public class User : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
    }
}
```

## Validation example usage

```csharp
using FluentValidation;
using Lina.Database.Models;
using Lina.ViewModels;

var user = new User()
{
    Name = ""
};

if (!await user.IsValid())
{
    Console.WriteLine("invalid");
}

user.Name = "Foo";

await user.Validate();

Console.WriteLine("Valid");

public class User : BaseValidateBaseEntity<User, UserValidation, int>
{
    public string Name { get; set; } = string.Empty;
}

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public record UserViewModel() : BaseViewModel<UserViewModel, UserViewModelValidation>
{
    public string Name { get; set; } = string.Empty;
}

public class UserViewModelValidation : AbstractValidator<UserViewModel>
{
    public UserViewModelValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
```

## Dependency injection example usage

```csharp
using Config.Net;
using FluentValidation;
using Lina.Database;
using Lina.Database.Interfaces;
using Lina.Database.Models;
using Lina.Database.Repositories;
using Lina.DynamicServicesProvider.Attributes;
using Lina.LoaderConfig;
using Lina.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

var config = serviceCollection.AddLoaderConfig<IAppConfig>();
serviceCollection.AddLinaDbContext<Program>((builder, assembly) =>
    builder.UseMySql(config.DatabaseUrl, ServerVersion.AutoDetect(config.DatabaseUrl),
        optionsBuilder => optionsBuilder.MigrationsAssembly(assembly)));

public interface IAppConfig 
{
    [Option(DefaultValue = "Server=localhost;Database=test;User Id=root;Password=root;")]
    public string DatabaseUrl { get; }
}

public class User : BaseValidateBaseEntity<User, UserValidation, int>
{
    public string Name { get; set; } = string.Empty;

    public static implicit operator User(UserViewModel viewModel)
    {
        return new User()
        {
            Name = viewModel.Name
        };
    }

    public static implicit operator UserViewModel(User user)
    {
        return new UserViewModel()
        {
            Name = user.Name
        };
    }
}

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
    }
}

public record UserViewModel() : BaseViewModel<UserViewModel, UserViewModelValidation>
{
    public string Name { get; set; } = string.Empty;
}

public class UserViewModelValidation : AbstractValidator<UserViewModel>
{
    public UserViewModelValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public interface IUserRepository : IBaseRepository<User, int>
{
}

[Repository(typeof(IUserRepository))]
public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

public interface IUserService
{
}

[Service(typeof(IUserService))]
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserViewModel?> Add(UserViewModel userViewModel)
    {
        if (!await userViewModel.IsValid())
        {
            throw new Exception("Not valid");
        }
        
        User user = userViewModel;
        await user.Validate();

        await _userRepository.Add(user);
        await _userRepository.Commit();

        return user;
    }
}
```

## Library usage

- [Config.Net](https://github.com/aloneguid/config)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
- [EntityFramework](https://learn.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://docs.automapper.org/en/stable/) (Obsolete)
