using Estimate.Application.Authentication.Dtos;
using FluentValidation;

namespace Estimate.Application.Authentication.Validators;

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