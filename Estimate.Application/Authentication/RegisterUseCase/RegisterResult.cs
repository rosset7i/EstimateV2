using Microsoft.AspNetCore.Identity;

namespace Estimate.Application.Authentication.RegisterUseCase;

public class RegisterResult
{
    public IdentityResult IdentityResult { get; init; }

    public RegisterResult(IdentityResult identityResult) =>
        IdentityResult = identityResult;
}