using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Authentication.LoginUseCase;

public record LoginCommand(
    string Email,
    string Password) : IRequest<ResultOf<LoginResult>>;
