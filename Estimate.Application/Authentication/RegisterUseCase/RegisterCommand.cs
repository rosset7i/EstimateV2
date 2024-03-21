using Estimate.Domain.Common.Errors;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Authentication.RegisterUseCase;

public class RegisterCommand : IRequest<ResultOf<RegisterResult>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
}