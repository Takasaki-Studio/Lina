namespace TakasakiStudio.Lina.Database.Interfaces;

public interface IBaseEntity<TPkType>
{
    /// <summary>
    /// Entity id
    /// </summary>
    public TPkType Id { get; set; }
    
    /// <summary>
    /// Create a clone of value
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    /// <returns></returns>
    public T Clone<T>()
    {
        return (T)MemberwiseClone();
    }
}