using Estimate.Api.ErrorHandling;
using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.Authentication.Controllers;

[Route("api/v1/authentication")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator) =>
        _mediator = mediator;

    [HttpPost("register")]
    public async Task<RegisterResult> RegisterAsync([FromBody]RegisterCommand command) =>
        await _mediator.Send(command);

    [HttpPost("login")]
    public async Task<LoginResult> LoginAsync([FromBody]LoginCommand command) =>
        await _mediator.Send(command);
}