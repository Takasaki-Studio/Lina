using AutoMapper;
using Lina.DynamicMapperConfiguration.Interfaces;

namespace Lina.DynamicMapperConfiguration.Abstracts;

/// <summary>
/// Base mapper
/// </summary>
/// <typeparam name="TOrigin">Model origin</typeparam>
/// <typeparam name="TDestiny">Model destiny</typeparam>
[Obsolete("Auto mapper discontinued")]
public abstract class DynamicMapperProfile<TOrigin, TDestiny> : Profile, IDynamicMapperProfile
{
    private readonly IMappingExpression<TOrigin, TDestiny> _mappingExpression;

    public DynamicMapperProfile()
    {
        _mappingExpression = CreateMap<TOrigin, TDestiny>();
    }
    
    public void LoadMap()
    {
        Map(_mappingExpression);
    }

    /// <summary>
    /// Map configuration
    /// </summary>
    /// <param name="mappingExpression">Map configuration expression</param>
    [Obsolete("Auto mapper discontinued")]
    protected abstract void Map(IMappingExpression<TOrigin, TDestiny> mappingExpression);
}