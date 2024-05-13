using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using TakasakiStudio.Lina.Common.Interfaces;

namespace TakasakiStudio.Lina.Common;

/// <summary>
/// Base class to view model with basic validation using <a href="https://docs.fluentvalidation.net/en/latest/">Fluent Validation</a>
/// </summary>
/// <typeparam name="TModel">Self ref class</typeparam>
public abstract class BaseValidator<TModel> : IValidate
{
    private LinaAbstractValidator<TModel>? _validator;

    protected BaseValidator()
    {
        InstanceValidator();
    }

    /// <summary>
    /// Validate view model with class validation and throw exception if failure
    /// </summary>
    /// <exception cref="ValidationException">Failure validation information</exception>
    public virtual async ValueTask Validate()
    {
        await _validator!.ValidateAndThrowAsync(GetClassInstance());
    }

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <returns>Failure validation information</returns>
    public virtual async Task<ValidationResult> GetErrors()
    {
        return await _validator!.ValidateAsync(GetClassInstance());
    }

    /// <summary>
    /// Verify if validation pass
    /// </summary>
    /// <returns>If valid</returns>
    public virtual async ValueTask<bool> IsValid()
    {
        return (await GetErrors()).IsValid;
    }

    public abstract void SetupValidator(LinaAbstractValidator<TModel> rules);

    private void InstanceValidator()
    {
        var setupValidator = (LinaAbstractValidator<TModel>.ValidationBuilder)SetupValidator;

        _validator = (LinaAbstractValidator<TModel>?)Activator.CreateInstance(
            type: typeof(LinaAbstractValidator<TModel>),
            bindingAttr: BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance,
            binder: null,
            args: [setupValidator],
            culture: null,
            activationAttributes: null);
    }

    private TModel GetClassInstance()
    {
        return (TModel)(object)this;
    }
}