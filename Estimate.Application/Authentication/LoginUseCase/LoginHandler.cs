using Estimate.Application.Authentication.Login;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Interface;
using Estimate.Infra.TokenFactory;

namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginHandler
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
    
    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.FetchByEmailAsync(request.Email);

        if (user is null)
            throw new BusinessException(DomainError.Authentication.WrongEmailOrPassword);

        var result = await _userRepository.LoginUsingPasswordAsync(
            user,
            request.Password,
            false,
            false);

        if (!result.Succeeded)
            throw new BusinessException(DomainError.Authentication.WrongEmailOrPassword);

        var token = _tokenGeneratorService.GenerateToken(user);

        var authResponse = new LoginResponse(
            user.Email,
            token);
        
        return authResponse;
    }
}