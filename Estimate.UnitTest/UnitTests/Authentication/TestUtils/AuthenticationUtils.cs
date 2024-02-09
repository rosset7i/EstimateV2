using Bogus;
using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Domain.Entities;

namespace Estimate.UnitTest.UnitTests.Authentication.TestUtils;

public static class AuthenticationUtils
{
    private static readonly Faker Faker = new();

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
        new(Faker.Internet.Email(),
            Faker.Lorem.Text());

    public static User CreateUser() =>
        new(Faker.Name.FirstName(),
            Faker.Internet.Email(),
            Faker.Phone.PhoneNumber("(##)#####-####"));
}