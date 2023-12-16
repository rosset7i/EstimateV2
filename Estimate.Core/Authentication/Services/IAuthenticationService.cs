using Estimate.Core.Authentication.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Core.Authentication.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}