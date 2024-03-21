using Bogus;
using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Domain.Entities;

namespace Estimate.UnitTest.UnitTests.Authentication.TestUtils;

public static class AuthenticationUtils
{
    public static RegisterCommand CreateRegisterRequest()
    {
        return new Faker<RegisterCommand>()
            .CustomInstantiator(f => new RegisterCommand(
                f.Name.FirstName(),
                f.Internet.Email(),
                f.Internet.Password(),
                f.Phone.PhoneNumber()));
    }

    public static LoginCommand CreateLoginCommand()
    {
        return new Faker<LoginCommand>()
            .CustomInstantiator(f => new LoginCommand(
                f.Internet.Email(),
                f.Internet.Password()));
    }

    public static LoginResult CreateLoginResponse()
    {
        return new Faker<LoginResult>()
            .CustomInstantiator(f => new LoginResult(
                f.Internet.Email(),
                f.Lorem.Text()));
    }

    public static User CreateUser()
    {
        return new Faker<User>()
            .CustomInstantiator(f => new User(
                f.Name.FindName(),
                f.Internet.Email(),
                f.Phone.PhoneNumber("(##)#####-####")));
    }
}