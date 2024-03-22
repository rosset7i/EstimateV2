using Estimate.Application.Authentication.LoginUseCase;
using Estimate.Application.Common;
using Estimate.Application.Common.Repositories;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Authentication.TestUtils;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Authentication;

public class LoginHandlerTests : IUnitTestBase<LoginHandler, LoginHandlerMocks>
{
    [Fact]
    public async Task Login_WhenUserDoesntExist_ShouldReturnError()
    {
        //Arrange
        var command = AuthenticationUtils.CreateLoginCommand();

        var mocks = GetMocks();
        var handle = GetClass(mocks);

        //Act
        var result = await handle.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Authentication.WrongEmailOrPassword, result.FirstError);
        mocks.ShouldCallFetchUserByEmail(command.Email)
            .ShouldNotCallLoginWithPassword()
            .ShouldNotCallGenerateToken();
    }

    [Fact]
    public async Task Login_WhenIdentityFails_ShouldReturnError()
    {
        //Arrange
        var command = AuthenticationUtils.CreateLoginCommand();
        var user = AuthenticationUtils.CreateUser();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.UserRepository.Setup(e => e
            .FetchByEmailAsync(command.Email))
            .ReturnsAsync(user);

        mocks.UserRepository.Setup(e => e
            .LoginUsingPasswordAsync(
                user,
                command.Password,
                false,
                false))
            .ReturnsAsync(SignInResult.Failed);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Authentication.WrongEmailOrPassword, result.FirstError);
        mocks.ShouldCallFetchUserByEmail(command.Email)
            .ShouldCallLoginWithPassword(user, command)
            .ShouldNotCallGenerateToken();
    }

    [Fact]
    public async Task Login_WhenPasswordMatch_ShouldReturnToken()
    {
        //Arrange
        var commmand = AuthenticationUtils.CreateLoginCommand();
        var loginResponse = AuthenticationUtils.CreateLoginResponse();
        var user = AuthenticationUtils.CreateUser();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.UserRepository.Setup(e => e
            .FetchByEmailAsync(commmand.Email))
            .ReturnsAsync(user);

        mocks.UserRepository.Setup(e => e
            .LoginUsingPasswordAsync(
                user,
                commmand.Password,
                false,
                false))
            .ReturnsAsync(SignInResult.Success);

        mocks.JtwTokenGeneratorService.Setup(e => e
            .GenerateToken(user))
            .Returns(loginResponse.Token);

        //Act
        var result = await handler.Handle(commmand, CancellationToken.None);

        //Assert
        Assert.Equivalent(loginResponse.Token, result.Result?.Token);
        mocks.ShouldCallFetchUserByEmail(commmand.Email)
            .ShouldCallLoginWithPassword(user, commmand)
            .ShouldCallGenerateToken(user);
    }

    public LoginHandlerMocks GetMocks()
    {
        return new LoginHandlerMocks(
            new Mock<IUserRepository>(),
            new Mock<IJwtTokenGeneratorService>());
    }

    public LoginHandler GetClass(LoginHandlerMocks mocks)
    {
        return new LoginHandler(
            mocks.JtwTokenGeneratorService.Object,
            mocks.UserRepository.Object);
    }
}

public class LoginHandlerMocks
{
    public Mock<IUserRepository> UserRepository { get; set; }
    public Mock<IJwtTokenGeneratorService> JtwTokenGeneratorService { get; set; }

    public LoginHandlerMocks(
        Mock<IUserRepository> userRepository,
        Mock<IJwtTokenGeneratorService> jtwTokenGeneratorService)
    {
        UserRepository = userRepository;
        JtwTokenGeneratorService = jtwTokenGeneratorService;
    }

    public LoginHandlerMocks ShouldCallLoginWithPassword(User user, LoginCommand command)
    {
        UserRepository
            .Verify(e => e
                .LoginUsingPasswordAsync(
                    user,
                    command.Password,
                    false,
                    false),
                Times.Once);

        return this;
    }

    public LoginHandlerMocks ShouldCallFetchUserByEmail(string email)
    {
        UserRepository
            .Verify(e => e
                .FetchByEmailAsync(email),
                Times.Once);

        return this;
    }

    public void ShouldCallGenerateToken(User user)
    {
        JtwTokenGeneratorService
            .Verify(e => e
                .GenerateToken(user),
                Times.Once);
    }

    public void ShouldNotCallGenerateToken()
    {
        JtwTokenGeneratorService
            .Verify(e => e.GenerateToken(It.IsAny<User>()),
                Times.Never);
    }

    public LoginHandlerMocks ShouldNotCallLoginWithPassword()
    {
        UserRepository
            .Verify(e => e
                .LoginUsingPasswordAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    false,
                    false),
                Times.Never);

        return this;
    }
}