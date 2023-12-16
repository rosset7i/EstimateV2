using Estimate.Core.Authentication.Dtos;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Infra.TokenFactory;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Core.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGeneratorService _tokenGeneratorService;

    public AuthenticationService(
        IJwtTokenGeneratorService tokenGeneratorService,
        IUserRepository userRepository)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _userRepository = userRepository;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var user = await _userRepository.FetchByEmailAsync(request.Email);

        if (user is not null)
            throw new BusinessException(DomainError.Authentication.EmailAlreadyInUse);

        var newUser = new User(
            request.Name,
            request.Email,
            request.Phone);

        var result = await _userRepository.CreateUserAsync(
            newUser,
            request.Password);

        if(!result.Succeeded)
            throw new BusinessException(DomainError.Authentication.RegisterError(result.Errors));

        return result;
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