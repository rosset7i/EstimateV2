using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginCommand : IRequest<ResultOf<LoginResult>>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public LoginCommand(
        string email,
        string password)
    {
        Email = email;
        Password = password;
    }
}