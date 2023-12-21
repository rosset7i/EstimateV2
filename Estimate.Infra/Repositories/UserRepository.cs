using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password) =>
        await _userManager.CreateAsync(user, password);

    public async Task<User?> FetchByEmailAsync(string email) =>
        await _userManager.FindByEmailAsync(email);

    public async Task<SignInResult> LoginUsingPasswordAsync(
        User user,
        string password,
        bool isPersistent,
        bool lockoutOnFailure) =>
        await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
}