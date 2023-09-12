using FluentValidation;
using FluentValidation.Results;
using Lina.Common.Interfaces;

namespace Lina.Database.Models;

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