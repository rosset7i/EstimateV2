using FluentValidation;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
{
    public UpdateSupplierValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(75);
    }
}