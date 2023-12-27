using FluentValidation;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

public class UpdateEstimateValidator : AbstractValidator<UpdateEstimateCommand>
{
    public UpdateEstimateValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}