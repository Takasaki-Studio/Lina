using FluentValidation;
using FluentValidation.Results;
using TakasakiStudio.Lina.Common;
using TakasakiStudio.Lina.Common.Interfaces;
using TakasakiStudio.Lina.Database.Interfaces;

namespace TakasakiStudio.Lina.Database.Models;

/// <summary>
/// Base entity with validation
/// </summary>
/// <typeparam name="TModel">Entity model</typeparam>
/// <typeparam name="TPkKey">Entity id type</typeparam>
public abstract class BaseEntityValidate<TModel, TPkKey> : BaseValidator<TModel>, IBaseEntity<TPkKey>
{
    public TPkKey Id { get; set; } = default!;
}