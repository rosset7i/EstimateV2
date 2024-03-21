using Estimate.Domain.Common;
using FluentValidation;
using FluentValidation.Results;
using Rossetti.Common.Result;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Domain.Entities.Base;

public abstract class ValueObject<TClass> : AbstractValidator<TClass>
{
    protected abstract void ValidateObject();

    protected void ThrowIfAny(ValidationResult validationResult)
    {
        if (validationResult.Errors.Any())
            throw new BusinessException(CommonError.InvalidDomain<TClass>());
    }
}