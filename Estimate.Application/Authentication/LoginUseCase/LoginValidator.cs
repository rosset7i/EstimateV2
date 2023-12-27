using FluentValidation;

namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}