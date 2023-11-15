using System.Reflection;
using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database;

/// <summary>
/// Reference assembly data for create dependency injection
/// </summary>
public class AssemblyEntityConfigurationInjection
    (Assembly configurationAssembly) : IAssemblyEntityConfigurationInjection
{
    public Assembly ConfigurationAssembly { get; } = configurationAssembly;
}