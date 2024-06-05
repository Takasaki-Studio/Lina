using FluentValidation;
using FluentValidation.Results;

namespace TakasakiStudio.Lina.Common.Interfaces;

/// <summary>
/// Interface for validate functions
/// </summary>
public interface IValidate
{
    /// <summary>
    /// Validate view model with class validation and throw exception if failure
    /// </summary>
    /// <param name="rules">Rule sets from validator</param>
    /// <exception cref="ValidationException">Failure validation information</exception>
    ValueTask Validate(params string[] rules);

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <param name="rules">Rule sets from validator</param>
    /// <returns>Failure validation information</returns>
    Task<ValidationResult> GetErrors(params string[] rules);

    /// <summary>
    /// Verify if valid
    /// </summary>
    /// <param name="rules">Rule sets from validator</param>
    /// <returns>If valid</returns>
    ValueTask<bool> IsValid(params string[] rules);
}