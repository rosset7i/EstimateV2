using Bogus;
using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Authentication.TestUtils;

public static class AuthenticationUtils
{
    public static RegisterCommand CreateRegisterRequest() =>
        new Faker<RegisterCommand>()
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.Password, f => f.Internet.Password())
            .Generate();

    public static LoginCommand CreateLoginCommand() =>
        new Faker<LoginCommand>()
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.Password, f => f.Internet.Password())
            .Generate();

    public static LoginResult CreateLoginResponse() =>
        new(Constants.User.Email,
            Constants.User.Token);

    public static User CreateUser() =>
        new(Constants.User.Name,
            Constants.User.Email,
            Constants.User.Phone);
}