using Estimate.Domain.Common.Errors;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginCommand : IRequest<ResultOf<LoginResult>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}