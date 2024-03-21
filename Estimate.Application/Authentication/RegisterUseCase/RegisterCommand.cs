using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Authentication.RegisterUseCase;

public record RegisterCommand(
    string Name,
    string Email,
    string Password,
    string Phone) : IRequest<ResultOf<RegisterResult>>;