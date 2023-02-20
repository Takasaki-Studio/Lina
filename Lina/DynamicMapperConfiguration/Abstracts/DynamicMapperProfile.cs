using AutoMapper;
using Lina.DynamicMapperConfiguration.Interfaces;

namespace Lina.DynamicMapperConfiguration.Abstracts;

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

    protected abstract void Map(IMappingExpression<TOrigin, TDestiny> mappingExpression);
}