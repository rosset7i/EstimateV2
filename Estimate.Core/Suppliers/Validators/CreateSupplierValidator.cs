using Estimate.Core.Suppliers.Dtos;
using FluentValidation;

namespace Estimate.Core.Suppliers.Validators;

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