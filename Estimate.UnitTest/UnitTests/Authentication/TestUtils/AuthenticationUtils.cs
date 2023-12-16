using Estimate.Core.Authentication.Dtos;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Authentication.TestUtils;

public static class AuthenticationUtils
{
    public static RegisterRequest CreateRegisterRequest() =>
        new(Constants.User.Name,
            Constants.User.Email,
            Constants.User.Password,
            Constants.User.Phone);
    
    public static LoginRequest CreateLoginRequest() =>
        new(Constants.User.Email,
            Constants.User.Password);
    
    public static LoginResponse CreateLoginResponse() =>
        new(Constants.User.Email,
            Constants.User.Token);

    public static User CreateUser() =>
        new(Constants.User.Name,
            Constants.User.Email,
            Constants.User.Phone);
}

