using Estimate.Application.Suppliers.Dtos;
using FluentValidation;

namespace Estimate.Application.Suppliers.Validators;

public class UpdateSupplierInfoValidator : AbstractValidator<UpdateSupplierInfoRequest>
{
    public UpdateSupplierInfoValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}