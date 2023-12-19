using Estimate.Application.Suppliers.Dtos;
using FluentValidation;

namespace Estimate.Application.Suppliers.Validators;

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