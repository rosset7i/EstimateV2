using Estimate.Application.Authentication.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Application.Authentication.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}