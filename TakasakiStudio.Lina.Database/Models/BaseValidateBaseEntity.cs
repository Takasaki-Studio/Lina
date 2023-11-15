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
public abstract class BaseValidateBaseEntity<TModel, TValidationClass, TPkKey> : BaseEntity<TPkKey>,
    IValidate,
    IBaseValidate<TModel, TValidationClass>
where TValidationClass: IValidator<TModel>
{
    public virtual async ValueTask Validate()
    {
        await GetBaseImplementationInstance().BaseValidate();
    }

    public virtual async Task<ValidationResult> GetErrors()
    {
        return await GetBaseImplementationInstance().BaseGetErrors();
    }

    public virtual async ValueTask<bool> IsValid()
    {
        return await GetBaseImplementationInstance().BaseIsValid();
    }

    private IBaseValidate<TModel, TValidationClass> GetBaseImplementationInstance() => this;
}