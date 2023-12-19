using FluentValidation;

namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}