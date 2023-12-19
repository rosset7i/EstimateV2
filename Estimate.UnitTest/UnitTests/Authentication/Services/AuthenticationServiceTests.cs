using Estimate.Application.Authentication.Login;
using Estimate.Application.Authentication.Register;
using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Infra.TokenFactory;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Authentication.TestUtils;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.UnitTest.UnitTests.Authentication.Services;

public class AuthenticationServiceTests : IUnitTestBase<RegisterHandler, AuthenticationServiceMocks>
{
    [Fact]
    public async Task Register_WhenEmailDoesntExist_ShouldSaveNewUser()
    {
        //Arrange
        var registerRequest = AuthenticationUtils.CreateRegisterRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.UserRepository
            .Setup(e => e.CreateUserAsync(
                It.IsAny<User>(),
                It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        //Act
        var result = await service.RegisterAsync(registerRequest);

        //Assert
        result.Succeeded
            .Should()
            .BeTrue();
        mocks.ShouldCallFetchUserByEmail(registerRequest.Email)
            .ShouldCallCreateUser();
    }
    
    [Fact]
    public async Task Register_WhenEmailAlreadyExists_ShouldReturnConflict()
    {
        //Arrange
        var user = AuthenticationUtils.CreateUser();
        var registerRequest = AuthenticationUtils.CreateRegisterRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.UserRepository
            .Setup(e => e.FetchByEmailAsync(user.Email))
            .ReturnsAsync(user);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.RegisterAsync(registerRequest));

        //Assert
        Assert.Equivalent(DomainError.Authentication.EmailAlreadyInUse, result.FirstError);
        mocks.ShouldCallFetchUserByEmail(user.Email)
            .ShouldNotCallCreateUser();
    }
    
    [Fact]
    public async Task Login_WhenUserWithEmailDoesntExist_ShouldReturnError()
    {
        //Arrange
        var loginRequest = AuthenticationUtils.CreateLoginRequest();

        loginRequest.Email = "";
        
        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.LoginAsync(loginRequest));

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
        var service = GetClass(mocks);

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
        var result = await Assert.ThrowsAsync<BusinessException>( () => service.LoginAsync(loginRequest));

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
        var service = GetClass(mocks);

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
        var result = await service.LoginAsync(loginRequest);

        //Assert
        result
            .Should()
            .BeEquivalentTo(loginResponse);
        mocks.ShouldCallFetchUserByEmail(loginRequest.Email)
            .ShouldCallLoginWithPassword(user, loginRequest)
            .ShouldCallGenerateToken(user);
    }

    public AuthenticationServiceMocks GetMocks()
    {
        return new AuthenticationServiceMocks(
            new Mock<IUserRepository>(),
            new Mock<IJwtTokenGeneratorService>());
    }

    public RegisterHandler GetClass(AuthenticationServiceMocks mocks)
    {
        return new RegisterHandler(
            mocks.JtwTokenGeneratorService.Object,
            mocks.UserRepository.Object);
    }
}

public class AuthenticationServiceMocks
{
    public Mock<IUserRepository> UserRepository { get; set; }
    public Mock<IJwtTokenGeneratorService> JtwTokenGeneratorService { get; set; }

    public AuthenticationServiceMocks(
        Mock<IUserRepository> userRepository,
        Mock<IJwtTokenGeneratorService> jtwTokenGeneratorService)
    {
        UserRepository = userRepository;
        JtwTokenGeneratorService = jtwTokenGeneratorService;
    }
    
    public void ShouldCallCreateUser()
    {
        UserRepository
            .Verify(e => e.CreateUserAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>()),
                Times.Once);
    }

    public AuthenticationServiceMocks ShouldCallLoginWithPassword(User user, LoginRequest request)
    {
        UserRepository
            .Verify(e => e
                    .LoginUsingPasswordAsync(
                        user,
                        request.Password,
                        false,
                        false),
                Times.Once);

        return this;
    }

    public AuthenticationServiceMocks ShouldCallFetchUserByEmail(string email)
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
    
    public void ShouldNotCallCreateUser()
    {
        UserRepository
            .Verify(e => e.CreateUserAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>()),
                Times.Never);
    }

    public AuthenticationServiceMocks ShouldNotCallLoginWithPassword()
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