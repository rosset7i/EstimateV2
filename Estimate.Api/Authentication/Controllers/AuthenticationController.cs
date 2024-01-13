using Estimate.Api.ErrorHandling;
using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.Authentication.Controllers;

[Route("api/v1/Authentication")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator) =>
        _mediator = mediator;

    [HttpPost("Register")]
    public async Task<ResultOf<RegisterResult>> RegisterAsync([FromBody] RegisterCommand command) =>
        await _mediator.Send(command);

    [HttpPost("Login")]
    public async Task<ResultOf<LoginResult>> LoginAsync([FromBody] LoginCommand command) =>
        await _mediator.Send(command);
}