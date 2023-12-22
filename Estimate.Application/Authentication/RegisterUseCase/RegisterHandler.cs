using Estimate.Application.Common.Repositories;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;

namespace Estimate.Application.Authentication.RegisterUseCase;

public class RegisterHandler : IRequestHandler<RegisterCommand, ResultOf<RegisterResult>>
{
    private readonly IUserRepository _userRepository;

    public RegisterHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultOf<RegisterResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FetchByEmailAsync(command.Email);

        if (user is not null)
            return DomainError.Authentication.EmailAlreadyInUse;

        var newUser = new User(
            command.Name,
            command.Email,
            command.Phone);

        var result = await _userRepository.CreateUserAsync(
            newUser,
            command.Password);

        if(!result.Succeeded)
            return DomainError.Authentication.RegisterError(result.Errors);

        return new RegisterResult(result);
    }
}