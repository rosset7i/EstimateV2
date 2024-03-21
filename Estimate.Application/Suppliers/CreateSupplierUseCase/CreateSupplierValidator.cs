using FluentValidation;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75);
    }
}