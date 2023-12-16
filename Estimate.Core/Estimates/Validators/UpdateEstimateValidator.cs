using Estimate.Core.Estimates.Dtos;
using FluentValidation;

namespace Estimate.Core.Estimates.Validators;

public class UpdateEstimateValidator : AbstractValidator<UpdateEstimateInfoRequest>
{
    public UpdateEstimateValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}