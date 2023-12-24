using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Authentication.RegisterUseCase;

public class RegisterCommand : IRequest<ResultOf<RegisterResult>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }

    public RegisterCommand(
        string name,
        string email,
        string password,
        string phone)
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
    }
}