using FluentValidation;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}