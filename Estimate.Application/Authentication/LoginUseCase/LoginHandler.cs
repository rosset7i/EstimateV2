using Estimate.Application.Infrastructure;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Interface;
using MediatR;

namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
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
    
    public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FetchByEmailAsync(command.Email);

        if (user is null)
            throw new BusinessException(DomainError.Authentication.WrongEmailOrPassword);

        var result = await _userRepository.LoginUsingPasswordAsync(
            user,
            command.Password,
            false,
            false);

        if (!result.Succeeded)
            throw new BusinessException(DomainError.Authentication.WrongEmailOrPassword);

        var token = _tokenGeneratorService.GenerateToken(user);

        var authResponse = new LoginResult(
            user.Email,
            token);
        
        return authResponse;
    }
}