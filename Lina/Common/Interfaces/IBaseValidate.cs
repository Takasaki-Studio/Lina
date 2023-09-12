using FluentValidation;
using FluentValidation.Results;

namespace Lina.Common.Interfaces;

public interface IBaseValidate<TModel, TValidationClass>
    where TValidationClass: IValidator<TModel>
{
    private static IValidator<TModel> GetValidationInstance() =>
        Activator.CreateInstance<TValidationClass>();
    
    public async ValueTask BaseValidate()
    {
        await GetValidationInstance().ValidateAndThrowAsync((TModel)this);
    }

    public async Task<ValidationResult> BaseGetErrors()
    {
        return await GetValidationInstance().ValidateAsync((TModel)this);
    }

    public async ValueTask<bool> BaseIsValid()
    {
        var resultValidation = await BaseGetErrors();
        return resultValidation.IsValid;
    }
}