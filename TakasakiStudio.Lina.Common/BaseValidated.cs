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
    private readonly LinaAbstractValidator<TModel> _validator;

    protected BaseValidated()
    {
        _validator = InstanceValidator();
    }

    /// <summary>
    /// Validate view model with class validation and throw exception if failure
    /// </summary>
    /// <param name="rules">Rule sets from validator</param>
    /// <exception cref="ValidationException">Failure validation information</exception>
    public virtual async ValueTask Validate(params string[] rules)
    {
        await _validator.ValidateAsync(GetClassInstance(), strategy =>
        {
            strategy.ThrowOnFailures().IncludeRuleSets(rules).IncludeRulesNotInRuleSet();
        });
    }

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <param name="rules">Rule sets from validator</param>
    /// <returns>Failure validation information</returns>
    public virtual async Task<ValidationResult> GetErrors(params string[] rules)
    {
        return await _validator.ValidateAsync(GetClassInstance(), strategy =>
        {
            strategy.IncludeRuleSets(rules).IncludeRulesNotInRuleSet();
        });
    }

    /// <summary>
    /// Verify if validation pass
    /// </summary>
    /// <param name="rules">Rule sets from validator</param>
    /// <returns>If valid</returns>
    public virtual async ValueTask<bool> IsValid(params string[] rules)
    {
        return (await GetErrors(rules)).IsValid;
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

    private LinaAbstractValidator<TModel> InstanceValidator()
    {
        var setupValidator = (LinaAbstractValidator<TModel>.ValidationBuilder)SetupValidator;
        return new LinaAbstractValidator<TModel>(setupValidator);
    }

    private TModel GetClassInstance()
    {
        return (TModel)(object)this;
    }
}