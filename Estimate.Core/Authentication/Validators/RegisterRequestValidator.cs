﻿using System.Text.RegularExpressions;
using Estimate.Core.Authentication.Dtos;
using FluentValidation;

namespace Estimate.Core.Authentication.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        var phoneRegex = new Regex(@"^\s*(\d{2}|\d{0})[-. ]?(\d{5}|\d{4})[-. ]?(\d{4})[-. ]?\s*$");

        RuleFor(e => e.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(e => e.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(100);

        RuleFor(e => e.Phone)
            .NotNull()
            .NotEmpty()
            .Matches(phoneRegex);

    }
}