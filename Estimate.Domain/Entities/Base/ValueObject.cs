using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using FluentValidation;
using FluentValidation.Results;

namespace Estimate.Domain.Entities.Base;

public abstract class ValueObject<TClass> : AbstractValidator<TClass>
{
    protected abstract void ValidateObject();

    protected void ThrowIfAny(ValidationResult validationResult)
    {
        if (validationResult.Errors.Any())
            throw new BusinessException(DomainError.Common.InvalidDomain<TClass>());
    }
}