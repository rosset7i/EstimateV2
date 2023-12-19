using Estimate.Application.Estimates.Dtos;
using FluentValidation;

namespace Estimate.Application.Estimates.Validators;

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