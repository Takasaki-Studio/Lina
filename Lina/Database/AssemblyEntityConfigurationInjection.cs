using System.Reflection;
using Lina.Database.Interfaces;

namespace Lina.Database;

/// <summary>
/// Reference assembly data for create dependency injection
/// </summary>
public class AssemblyEntityConfigurationInjection : IAssemblyEntityConfigurationInjection
{
    public AssemblyEntityConfigurationInjection(Assembly configurationAssembly)
    {
        ConfigurationAssembly = configurationAssembly;
    }

    public Assembly ConfigurationAssembly { get; }
}