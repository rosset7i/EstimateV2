using Estimate.Application.Products.UpdateProductUseCase;
using FluentValidation;

namespace Estimate.Application.Products.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}