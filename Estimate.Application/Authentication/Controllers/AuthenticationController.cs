using Estimate.Application.ErrorHandling;
using Estimate.Core.Authentication.Dtos;
using Estimate.Core.Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Application.Authentication.Controllers;

[Route("api/v1/authentication")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IdentityResult> RegisterAsync([FromBody]RegisterRequest request) =>
        await _authenticationService.RegisterAsync(request);

    [HttpPost("login")]
    public async Task<LoginResponse> LoginAsync([FromBody] LoginRequest request) =>
        await _authenticationService.LoginAsync(request);
}