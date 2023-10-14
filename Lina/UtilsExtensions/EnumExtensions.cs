using System.ComponentModel;

namespace Lina.UtilsExtensions;

public static class EnumExtensions
{
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

    public static string GetValue<T>(this T? value)
        where T : struct, Enum
    {
        return value is null ? "" : Convert.ToInt32(value).ToString();
    }
}