using Estimate.Application.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Interface;
using MediatR;

namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginHandler : IRequestHandler<LoginCommand, ResultOf<LoginResult>>
{
    private readonly IJwtTokenGeneratorService _tokenGeneratorService;
    private readonly IUserRepository _userRepository;

    public LoginHandler(
        IJwtTokenGeneratorService tokenGeneratorService,
        IUserRepository userRepository)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _userRepository = userRepository;
    }
    
    public async Task<ResultOf<LoginResult>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FetchByEmailAsync(command.Email);

        if (user is null)
            return DomainError.Authentication.WrongEmailOrPassword;

        var result = await _userRepository.LoginUsingPasswordAsync(
            user,
            command.Password,
            false,
            false);

        if (!result.Succeeded)
            return DomainError.Authentication.WrongEmailOrPassword;

        var token = _tokenGeneratorService.GenerateToken(user);

        var authResponse = new LoginResult(
            user.Email!,
            token);
        
        return authResponse;
    }
}