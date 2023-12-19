using FluentValidation;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public class UpdateEstimateProductsValidator : AbstractValidator<UpdateEstimateProductsRequest>
{
    public UpdateEstimateProductsValidator()
    {
        RuleFor(e => e.ProductId)
            .NotEqual(Guid.Empty)
            .NotNull();

        RuleFor(e => e.UnitPrice)
            .NotNull()
            .NotEmpty();

        RuleFor(e => e.Quantity)
            .NotNull()
            .NotEmpty();
    }
}