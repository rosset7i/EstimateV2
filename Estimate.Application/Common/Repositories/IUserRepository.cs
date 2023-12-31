﻿using Estimate.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Estimate.Application.Common.Repositories;

public interface IUserRepository
{
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<User?> FetchByEmailAsync(string email);
    Task<SignInResult> LoginUsingPasswordAsync(
        User user,
        string password,
        bool isPersistent,
        bool lockoutOnFailure);
}