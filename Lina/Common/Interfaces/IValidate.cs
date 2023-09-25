using FluentValidation.Results;

namespace Lina.Common.Interfaces;

/// <summary>
/// Interface for validate functions
/// </summary>
public interface IValidate
{
    /// <summary>
    /// Validate model
    /// </summary>
    ValueTask Validate();

    /// <summary>
    /// Get validation errors
    /// </summary>
    /// <returns>Failure validation information</returns>
    Task<ValidationResult> GetErrors();

    /// <summary>
    /// Verify if valid
    /// </summary>
    /// <returns>If valid</returns>
    ValueTask<bool> IsValid();
}