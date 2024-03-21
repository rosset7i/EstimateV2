using FluentValidation;

namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75);
    }
}