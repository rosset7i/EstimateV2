using Estimate.Application.Authentication.Login;
using Estimate.Application.Authentication.Register;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Infra.TokenFactory;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Application.Authentication.RegisterUseCase;

public class RegisterHandler
{
    private readonly IUserRepository _userRepository;

    public RegisterHandler(IUserRepository userRepository)
    {
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
}