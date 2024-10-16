namespace TakasakiStudio.Lina.Utils.Helpers;

/// <summary>
/// Delegate debounce type
/// </summary>
/// <typeparam name="TData">Data type</typeparam>
public delegate void Debounce<in TData>(TData data);