using System.Reflection;

namespace Lina.Database.Interfaces;

public interface IAssemblyEntityConfigurationInjection
{
    Assembly ConfigurationAssembly { get; }
}