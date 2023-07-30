using System.Reflection;
using Lina.Database.Interfaces;

namespace Lina.Database;

public class AssemblyEntityConfigurationInjection : IAssemblyEntityConfigurationInjection
{
    public AssemblyEntityConfigurationInjection(Assembly configurationAssembly)
    {
        ConfigurationAssembly = configurationAssembly;
    }

    public Assembly ConfigurationAssembly { get; }
}