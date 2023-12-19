using FluentValidation;

namespace Estimate.Application.Authentication.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}