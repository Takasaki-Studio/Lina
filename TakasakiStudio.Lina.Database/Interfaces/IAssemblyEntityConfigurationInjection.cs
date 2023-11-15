using System.Reflection;

namespace TakasakiStudio.Lina.Database.Interfaces;

/// <summary>
/// Assembly entity reference
/// </summary>
public interface IAssemblyEntityConfigurationInjection
{
    /// <summary>
    /// Assembly reference
    /// </summary>
    Assembly ConfigurationAssembly { get; }
}