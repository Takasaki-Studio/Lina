namespace TakasakiStudio.Lina.Utils.Helpers;

/// <summary>
/// Delegate throttle type
/// </summary>
/// <typeparam name="TData">Data type</typeparam>
public delegate void Throttle<in TData>(TData data);