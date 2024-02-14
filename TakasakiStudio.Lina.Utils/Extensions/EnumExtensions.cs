using System.ComponentModel;

namespace TakasakiStudio.Lina.Utils.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Get enum description or string name
    /// </summary>
    /// <param name="value">Enum value</param>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>Description</returns>
    public static string? GetDescription<T>(this T value) where T : struct
    {
        var type = typeof(T);

        var name = Enum.GetName(type, value);

        if (string.IsNullOrWhiteSpace(name))
            return value.ToString();
        
        var descriptionAttribute =
            (DescriptionAttribute[]?)type.GetField(name)
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return descriptionAttribute?.FirstOrDefault()?.Description ?? value.ToString();
    }
    
    /// <summary>
    /// Convert enum as option
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>Options</returns>
    public static IDictionary<string, string> GetOptions<T>()
        where T : struct, Enum
    {
        var underlyingType = Enum.GetUnderlyingType(typeof(T));
        var type = typeof(T);
        return Enum.GetValues<T>()
            .Select(x =>
            {
                var name = Enum.GetName(type, x);
                if (string.IsNullOrWhiteSpace(name)) return (x.ToString(), Convert.ChangeType(x, underlyingType));
                var descriptionAttribute =
                    (DescriptionAttribute[]?)type.GetField(name)
                        ?.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (descriptionAttribute?.FirstOrDefault()?.Description ?? x.ToString(),
                    Convert.ChangeType(x, underlyingType));
            })
            .ToDictionary(x => x.Item2.ToString()!, y => y.Item1.ToString());
    }

    /// <summary>
    /// Get enum value
    /// </summary>
    /// <param name="value">Enum</param>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>Value</returns>
    public static string GetValue<T>(this T? value)
        where T : struct, Enum
    {
        return value is null ? "" : Convert.ToInt32(value).ToString();
    }

    /// <summary>
    /// Unify flag list to unique value
    /// </summary>
    /// <param name="list">Flag list</param>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>Enum value</returns>
    public static T ToFlags<T>(this IEnumerable<T> list)
        where T : struct, Enum
    {
        var result = 0UL;
        var underlyingType = Enum.GetUnderlyingType(typeof(T));
        foreach (var flag in list)
        {
            var flagInNumber = Convert.ChangeType(flag, underlyingType).ToString();
            if (flagInNumber is not null)
            {
                result |= uint.Parse(flagInNumber);
            }
        }

        return (T)Enum.ToObject(typeof(T), Convert.ChangeType(result, underlyingType));
    }

    /// <summary>
    /// Split enum flag into list
    /// </summary>
    /// <param name="flag">Enum value</param>
    /// <typeparam name="T">Enum type</typeparam>
    /// <returns>Flag list</returns>
    public static IEnumerable<T> ToEnumerable<T>(this T flag)
        where T : struct, Enum
    {
        var flagInNumber = Convert.ToUInt64(flag);
        var underlyingType = Enum.GetUnderlyingType(typeof(T));
        return Enum.GetValues<T>()
            .Select(x =>
            {
                var stringConverted = Convert.ChangeType(x, underlyingType).ToString();
                return ulong.Parse(stringConverted ?? "0");
            })
            .Where(x => (x & flagInNumber) == x)
            .Select(x => (T)Enum.ToObject(typeof(T), x));
    }
}