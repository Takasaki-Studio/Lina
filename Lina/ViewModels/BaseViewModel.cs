using FluentValidation;
using FluentValidation.Results;
using Lina.Common.Interfaces;

namespace Lina.ViewModels;

/// <summary>
/// Base class to view model with basic validation with <a href="https://docs.fluentvalidation.net/en/latest/">Fluent Validation</a>
/// </summary>
/// <typeparam name="TModel">Self ref class</typeparam>
/// <typeparam name="TValidationClass">Validation class implementation</typeparam>
public abstract record BaseViewModel<TModel, TValidationClass> : IValidate, IBaseValidate<TModel, TValidationClass>
where TValidationClass : IValidator<TModel>
{
    /// <summary>
    /// Validate view model with class validation and throw exception if failure
    /// </summary>
    /// <exception cref="ValidationException">Failure validation information</exception>
    public virtual async ValueTask Validate()
    {
        await GetBaseImplementationInstance().BaseValidate();
    }

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <returns>Failure validation information</returns>
    public virtual async Task<ValidationResult> GetErrors()
    {
        return await GetBaseImplementationInstance().BaseGetErrors();
    }

    /// <summary>
    /// Verify if validation pass
    /// </summary>
    /// <returns>If valid</returns>
    public virtual async ValueTask<bool> IsValid()
    {
        return await GetBaseImplementationInstance().BaseIsValid();
    }
    
    private IBaseValidate<TModel, TValidationClass> GetBaseImplementationInstance() => this;
}