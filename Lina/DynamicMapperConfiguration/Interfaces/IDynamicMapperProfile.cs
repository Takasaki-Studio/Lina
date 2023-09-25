namespace Lina.DynamicMapperConfiguration.Interfaces;

/// <summary>
/// Interface mapper profile
/// </summary>
public interface IDynamicMapperProfile
{
    /// <summary>
    /// Load mapping into <see cref="AutoMapper.IMapper"/>
    /// </summary>
    void LoadMap();
}