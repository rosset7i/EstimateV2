using Estimate.Application.Estimates.Dtos;
using FluentValidation;

namespace Estimate.Application.Estimates.Validators;

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