using FluentValidation;
using FluentValidation.Results;

namespace Lina.Common.Interfaces;

/// <summary>
/// Base class with basic validation using <a href="https://docs.fluentvalidation.net/en/latest/">Fluent Validation</a>
/// </summary>
/// <typeparam name="TModel">Self ref class</typeparam>
/// <typeparam name="TValidationClass">Validation class implementation</typeparam>
public interface IBaseValidate<TModel, TValidationClass>
    where TValidationClass: IValidator<TModel>
{
    private static IValidator<TModel> GetValidationInstance() =>
        Activator.CreateInstance<TValidationClass>();
    
    /// <summary>
    /// Validate model with class validation and throw exception if failure
    /// </summary>
    /// <exception cref="ValidationException">Failure validation information</exception>
    public async ValueTask BaseValidate()
    {
        await GetValidationInstance().ValidateAndThrowAsync((TModel)this);
    }

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <returns>Failure validation information</returns>
    public async Task<ValidationResult> BaseGetErrors()
    {
        return await GetValidationInstance().ValidateAsync((TModel)this);
    }

    /// <summary>
    /// Verify if validation pass
    /// </summary>
    /// <returns>If valid</returns>
    public async ValueTask<bool> BaseIsValid()
    {
        var resultValidation = await BaseGetErrors();
        return resultValidation.IsValid;
    }
}