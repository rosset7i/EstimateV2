using Estimate.Application.Estimates.Dtos;
using FluentValidation;

namespace Estimate.Application.Estimates.Validators;

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