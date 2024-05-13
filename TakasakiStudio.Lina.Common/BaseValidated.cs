using FluentValidation;
using FluentValidation.Results;
using TakasakiStudio.Lina.Common.Interfaces;

namespace TakasakiStudio.Lina.Common;

/// <summary>
/// Base class to view model with basic validation using <a href="https://docs.fluentvalidation.net/en/latest/">Fluent Validation</a>
/// </summary>
/// <typeparam name="TModel">Self ref class</typeparam>
public abstract class BaseValidated<TModel> : IValidate
{
    private LinaAbstractValidator<TModel>? _validator;

    protected BaseValidated()
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

    /// <summary>
    /// Setups the validator rules
    /// </summary>
    /// <param name="rules">The validator instance, used to configure the validation rules</param>
    /// <see href="https://docs.fluentvalidation.net/en/latest/index.html">Fluent Validation documentation</see>
    /// <example>
    /// <code language="cs">
    /// public class ExampleModel : BaseValidated&lt;ExampleModel>
    /// {
    ///    public required string Test { get; set; }
    ///
    ///    protected override void SetupValidator(LinaAbstractValidator&lt;ExampleModel> rules)
    ///    {
    ///        rules.RuleFor(x => x.Test).NotEmpty().NotNull();
    ///    }
    /// }
    /// </code>
    /// </example>
    protected abstract void SetupValidator(LinaAbstractValidator<TModel> rules);

    private void InstanceValidator()
    {
        var setupValidator = (LinaAbstractValidator<TModel>.ValidationBuilder)SetupValidator;
        _validator = new LinaAbstractValidator<TModel>(setupValidator);
    }

    private TModel GetClassInstance()
    {
        return (TModel)(object)this;
    }
}