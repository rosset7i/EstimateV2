using FluentValidation;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

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