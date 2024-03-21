using FluentValidation;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75);
    }
}