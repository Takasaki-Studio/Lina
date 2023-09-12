using FluentValidation.Results;

namespace Lina.Common.Interfaces;

public interface IValidate
{
    ValueTask Validate();

    Task<ValidationResult> GetErrors();

    ValueTask<bool> IsValid();
}