using System.Text.RegularExpressions;
using FluentValidation;

namespace Estimate.Application.Authentication.RegisterUseCase;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        var phoneRegex = new Regex(@"^\s*(\d{2}|\d{0})[-. ]?(\d{5}|\d{4})[-. ]?(\d{4})[-. ]?\s*$");

        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(100);

        RuleFor(e => e.Phone)
            .NotEmpty()
            .Matches(phoneRegex);

        RuleFor(e => e.Password)
            .NotEmpty();
    }
}