using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Authentication.TestUtils;

public static class AuthenticationUtils
{
    public static RegisterCommand CreateRegisterRequest() =>
        new(Constants.User.Name,
            Constants.User.Email,
            Constants.User.Password,
            Constants.User.Phone);

    public static LoginCommand CreateLoginRequest() =>
        new(Constants.User.Email,
            Constants.User.Password);

    public static LoginResult CreateLoginResponse() =>
        new(Constants.User.Email,
            Constants.User.Token);

    public static User CreateUser() =>
        new(Constants.User.Name,
            Constants.User.Email,
            Constants.User.Phone);
}

