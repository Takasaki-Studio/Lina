using FluentValidation;
using FluentValidation.Results;
using TakasakiStudio.Lina.Common.Interfaces;

namespace TakasakiStudio.Lina.Database.Models;

/// <summary>
/// Base entity with validation
/// </summary>
/// <typeparam name="TModel">Entity model</typeparam>
/// <typeparam name="TValidationClass">Entity model validation</typeparam>
/// <typeparam name="TPkKey">Entity id type</typeparam>
public abstract class BaseEntityValidate<TModel, TValidationClass, TPkKey> : BaseEntity<TPkKey>,
    IValidate,
    IBaseValidate<TModel, TValidationClass>
where TValidationClass: IValidator<TModel>
{
    /// <summary>
    /// Validate entity and throw if invalid
    /// </summary>
    public virtual async ValueTask Validate()
    {
        await GetBaseImplementationInstance().BaseValidate();
    }

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <returns>Errors</returns>
    public virtual async Task<ValidationResult> GetErrors()
    {
        return await GetBaseImplementationInstance().BaseGetErrors();
    }

    /// <summary>
    /// Entity is valid
    /// </summary>
    /// <returns>Is valid</returns>
    public virtual async ValueTask<bool> IsValid()
    {
        return await GetBaseImplementationInstance().BaseIsValid();
    }

    private IBaseValidate<TModel, TValidationClass> GetBaseImplementationInstance() => this;
}