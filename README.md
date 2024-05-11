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
  - CPF and CNPJ validators
- Dependency Injection
  - Life time configurable
  - Easy manipulation
  - Services, Repositories, HttpClient and more types of preconfigured dependencies
- Asp Net Core
  - Blazor render component in controller
  - Clear hosted lifecycle
  - File version provider

## Example simple usage

```csharp
using Config.Net;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;
using TakasakiStudio.Lina.Utils.LoaderConfig;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddLoaderConfig<IAppConfig>();
serviceCollection.AddAutoDependencyInjection<Program>();

var services = serviceCollection.BuildServiceProvider();

services.GetRequiredService<IFooService>().PrintAppName();

public interface IFooService 
{
    public void PrintAppName();
}

[Service<IFooService>]
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
using TakasakiStudio.Lina.Utils.LoaderConfig;
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
using TakasakiStudio.Lina.Database;
using TakasakiStudio.Lina.Database.Models;
using TakasakiStudio.Lina.Utils.LoaderConfig;
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
using TakasakiStudio.Lina.Common;
using TakasakiStudio.Lina.Common.Extensions;
using TakasakiStudio.Lina.Database.Models;

var user = new User()
{
    Name = ""
};

if (!await user.IsValid())
{
    Console.WriteLine("invalid");
}

user.Name = "Foo";
user.Cpf = "349.306.930-80";
user.Cnpj = "82.099.001/0001-08";

await user.Validate();

Console.WriteLine("Valid");

public class User : BaseEntityValidate<User, UserValidation, int>
{
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    public required string Cnpj { get; set; }
}

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cpf).ValidCpf();
        RuleFor(x => x.Cnpj).ValidCnpj();
    }
}

public record UserViewModel(string Name, string Cpf, string Cnpj)
    : BaseValidationRecord<UserViewModel, UserViewModelValidation>;

public class UserViewModelValidation : AbstractValidator<UserViewModel>
{
    public UserViewModelValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cpf).ValidCpf();
        RuleFor(x => x.Cnpj).ValidCnpj();
    }
}
```

## Dependency injection example usage

```csharp
using Config.Net;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection;
using TakasakiStudio.Lina.AutoDependencyInjection.Attributes;
using TakasakiStudio.Lina.Common;
using TakasakiStudio.Lina.Database;
using TakasakiStudio.Lina.Database.Interfaces;
using TakasakiStudio.Lina.Database.Models;
using TakasakiStudio.Lina.Database.Repositories;
using TakasakiStudio.Lina.Utils.LoaderConfig;

var serviceCollection = new ServiceCollection();

var config = serviceCollection.AddLoaderConfig<IAppConfig>();
serviceCollection.AddAutoDependencyInjection<Program>();
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
        return new User
        {
            Name = viewModel.Name
        };
    }

    public static implicit operator UserViewModel(User user)
    {
        return new UserViewModel
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

public record UserViewModel : BaseValidationRecord<UserViewModel, UserViewModelValidation>
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

[Repository<IUserRepository>]
public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }
}

public interface IUserService
{
}

[Service<IUserService>]
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserViewModel?> Add(UserViewModel userViewModel)
    {
        if (!await userViewModel.IsValid()) throw new Exception("Not valid");

        User user = userViewModel;
        await user.Validate();

        await _userRepository.Add(user);
        await _userRepository.Commit();

        return user;
    }
}
```

## Blazor components

```csharp
using Microsoft.AspNetCore.Mvc;
using TakasakiStudio.Lina.AspNet.Controllers;

[Controller]
public class AuthController() : PageController
{
    [HttpGet]
    public IActionResult Login()
    {
        return RenderComponent<LoginPage>(); // LoginPage is a Blazor component
    }
}
```

## Clear hosted lifecycle

```csharp
using TakasakiStudio.Lina.AspNet.Workers;

public class MyWorker : AbstractHostedLifecycleService
{
    public override Task StartingAsync(CancellationToken cancellationToken)
    {
        /*...*/
    }
}
```

## Libraries usage

- [Config.Net](https://github.com/aloneguid/config)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
- [EntityFramework](https://learn.microsoft.com/en-us/ef/core/)

## License
The entire project, except for the file [FileVersionProvider.cs](TakasakiStudio.Lina.AspNet/Providers/FileVersionProvider.cs) is licensed under the [The Unlicense license](LICENSE).
The file FileVersionProvider.cs was copied from [Asp.NET Core](https://github.com/dotnet/aspnetcore/blob/6dfaf9e2cff6cfa3aab0b7842fe02fe9f71e0f60/src/Mvc/Mvc.Razor/src/Infrastructure/DefaultFileVersionProvider.cs) under the MIT License.