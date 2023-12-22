using FluentValidation;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierInfoRequest>
{
    public UpdateSupplierValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(75);
    }
}