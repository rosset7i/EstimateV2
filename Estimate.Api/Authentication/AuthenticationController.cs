using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rossetti.Common.Controller;
using Rossetti.Common.Result;

namespace Estimate.Api.Authentication;

[Route("api/v1/Authentication")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator) =>
        _mediator = mediator;

    [HttpPost("Register")]
    public async Task<ResultOf<RegisterResult>> RegisterAsync(
        [FromBody] RegisterCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpPost("Login")]
    public async Task<ResultOf<LoginResult>> LoginAsync(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);
}