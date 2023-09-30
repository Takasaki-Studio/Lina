namespace Lina.DynamicMapperConfiguration.Interfaces;

/// <summary>
/// Interface mapper profile
/// </summary>
[Obsolete("Auto mapper discontinued")]
public interface IDynamicMapperProfile
{
    /// <summary>
    /// Load mapping into <see cref="AutoMapper.IMapper"/>
    /// </summary>
    [Obsolete("Auto mapper discontinued")]
    void LoadMap();
}