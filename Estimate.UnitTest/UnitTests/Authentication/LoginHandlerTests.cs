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
    public async Task Login_WhenUserWithEmailDoesntExist_ShouldReturnError()
    {
        //Arrange
        var loginRequest = AuthenticationUtils.CreateLoginRequest();

        loginRequest.Email = string.Empty;
        
        var mocks = GetMocks();
        var handle = GetClass(mocks);

        //Act
        var result = await handle.Handle(loginRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Authentication.WrongEmailOrPassword, result.FirstError);
        mocks.ShouldCallFetchUserByEmail(loginRequest.Email)
            .ShouldNotCallLoginWithPassword()
            .ShouldNotCallGenerateToken();
    }
    
    [Fact]
    public async Task Login_WhenThePasswordDoesntMatch_ShouldReturnError()
    {
        //Arrange
        var loginRequest = AuthenticationUtils.CreateLoginRequest();
        var user = AuthenticationUtils.CreateUser();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.UserRepository.Setup(e => e
            .FetchByEmailAsync(loginRequest.Email))
            .ReturnsAsync(user);

        mocks.UserRepository.Setup(e => e
            .LoginUsingPasswordAsync(
                user,
                loginRequest.Password,
                false,
                false))
            .ReturnsAsync(SignInResult.Failed);

        //Act
        var result = await handler.Handle(loginRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Authentication.WrongEmailOrPassword, result.FirstError);
        mocks.ShouldCallFetchUserByEmail(loginRequest.Email)
            .ShouldCallLoginWithPassword(user, loginRequest)
            .ShouldNotCallGenerateToken();
    }

    [Fact]
    public async Task Login_WhenPasswordMatch_ShouldReturnToken()
    {
        //Arrange
        var loginRequest = AuthenticationUtils.CreateLoginRequest();
        var loginResponse = AuthenticationUtils.CreateLoginResponse();
        var user = AuthenticationUtils.CreateUser();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.UserRepository.Setup(e => e
            .FetchByEmailAsync(loginRequest.Email))
            .ReturnsAsync(user);

        mocks.UserRepository.Setup(e => e
            .LoginUsingPasswordAsync(
                user,
                loginRequest.Password,
                false,
                false))
            .ReturnsAsync(SignInResult.Success);

        mocks.JtwTokenGeneratorService.Setup(e => e
            .GenerateToken(user))
            .Returns(loginResponse.Token);

        //Act
        var result = await handler.Handle(loginRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(loginResponse, result.Result);
        mocks.ShouldCallFetchUserByEmail(loginRequest.Email)
            .ShouldCallLoginWithPassword(user, loginRequest)
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
            .Verify(e => e
                    .GenerateToken(
                        It.IsAny<User>()),
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