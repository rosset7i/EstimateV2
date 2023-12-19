using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using FluentValidation;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateValidator : AbstractValidator<CreateEstimateRequest>
{
    public CreateEstimateValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);

        RuleFor(e => e.SupplierId)
            .NotEqual(Guid.Empty)
            .NotNull();

        RuleForEach(e => e.ProductsInEstimate)
            .SetValidator(new UpdateEstimateProductsValidator());
    }
}