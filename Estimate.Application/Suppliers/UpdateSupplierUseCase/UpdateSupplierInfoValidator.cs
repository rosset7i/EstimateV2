using FluentValidation;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

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