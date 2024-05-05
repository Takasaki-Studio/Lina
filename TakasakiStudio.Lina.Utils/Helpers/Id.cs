namespace TakasakiStudio.Lina.Utils.Helpers;

public static class Id
{
    public static string Generate()
    {
        return Guid.NewGuid().ToString("N");
    }
}