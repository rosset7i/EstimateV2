using Estimate.Core.Suppliers.Dtos;
using FluentValidation;

namespace Estimate.Core.Suppliers.Validators;

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