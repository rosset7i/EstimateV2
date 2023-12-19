using Estimate.Api.ErrorHandling;
using Estimate.Application.Authentication.Login;
using Estimate.Application.Authentication.Register;
using Estimate.Application.Authentication.Register.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.Authentication.Controllers;

[Route("api/v1/authentication")]
public class AuthenticationController : ApiController
{
    private readonly IRegisterService _registerService;

    public AuthenticationController(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    [HttpPost("register")]
    public async Task<IdentityResult> RegisterAsync([FromBody]RegisterRequest request) =>
        await _registerService.RegisterAsync(request);

    [HttpPost("login")]
    public async Task<LoginResponse> LoginAsync([FromBody] LoginRequest request) =>
        await _registerService.LoginAsync(request);
}