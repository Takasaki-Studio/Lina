using System.Reflection;
using AutoMapper;
using Lina.DynamicMapperConfiguration.Abstracts;
using Lina.DynamicMapperConfiguration.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Lina.DynamicMapperConfiguration;

public static class DynamicMapperExtension
{
    public static void AddDynamicMappers<T>(this IServiceCollection services)
    {
        var assembly = typeof(T).Assembly;

        var mapperClasses = GetAllDescendantsOf(assembly, typeof(DynamicMapperProfile<,>));
        
        var mapperConfig = new MapperConfiguration(opc =>
        {
            foreach (var mapperClass in mapperClasses)
            {
                var instancedConstructor = mapperClass.GetConstructors().First();
                var instancedMapper = (IDynamicMapperProfile)instancedConstructor.Invoke(Array.Empty<object>());
                instancedMapper.LoadMap();
            
                opc.AddProfile((Profile)instancedMapper);
            }
        });
        
        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
    
    private static IEnumerable<Type> GetAllDescendantsOf(
        this Assembly assembly, 
        Type genericTypeDefinition)
    {
        IEnumerable<Type> GetAllAscendants(Type t)
        {
            var current = t;

            while (current.BaseType is not null && current.BaseType != typeof(object))
            {
                yield return current.BaseType;
                current = current.BaseType;
            }
        }

        if (assembly == null)
            throw new ArgumentNullException(nameof(assembly));

        if (genericTypeDefinition == null)
            throw new ArgumentNullException(nameof(genericTypeDefinition));

        if (!genericTypeDefinition.IsGenericTypeDefinition)
            throw new ArgumentException(
                "Specified type is not a valid generic type definition.", 
                nameof(genericTypeDefinition));

        return assembly.GetTypes()
            .Where(t => GetAllAscendants(t).Any(d =>
                d.IsGenericType &&
                d.GetGenericTypeDefinition()
                    .Equals(genericTypeDefinition)));
    }
}