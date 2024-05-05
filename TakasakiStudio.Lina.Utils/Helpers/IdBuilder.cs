namespace TakasakiStudio.Lina.Utils.Helpers;

public static class IdBuilder
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString("N");
    }
}