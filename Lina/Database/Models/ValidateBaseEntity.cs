using FluentValidation;
using FluentValidation.Results;

namespace Lina.Database.Models;

public abstract class ValidateBaseEntity<TModel, TValidationClass, TPkKey> : BaseEntity<TPkKey>
where TValidationClass: IValidator<TModel>
{
    private readonly IValidator<TModel> _validator;

    protected ValidateBaseEntity()
    {
        _validator = Activator.CreateInstance<TValidationClass>();
    }

    public virtual async ValueTask Validate()
    {
        await _validator.ValidateAndThrowAsync((TModel)(object)this);
    }

    public virtual async Task<ValidationResult> GetErrors()
    {
        return await _validator.ValidateAsync((TModel)(object)this);
    }

    public virtual async ValueTask<bool> IsValid()
    {
        var resultValidation = await GetErrors();
        return resultValidation.IsValid;
    }
}