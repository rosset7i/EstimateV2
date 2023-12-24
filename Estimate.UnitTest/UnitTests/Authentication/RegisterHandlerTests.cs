using Estimate.Application.Authentication.RegisterUseCase;
using Estimate.Application.Common.Repositories;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Authentication.TestUtils;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Authentication;

public class RegisterHandlerTests : IUnitTestBase<RegisterHandler, RegisterHandlerMocks>
{
    [Fact]
    public async Task Register_WhenEmailDoesntExist_ShouldSaveNewUser()
    {
        //Arrange
        var registerRequest = AuthenticationUtils.CreateRegisterRequest();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.UserRepository
            .Setup(e => e.CreateUserAsync(
                It.IsAny<User>(),
                It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        //Act
        var result = await handler.Handle(registerRequest, CancellationToken.None);

        //Assert
        Assert.Equal(IdentityResult.Success, result.Result.IdentityResult);
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
        var handler = GetClass(mocks);

        mocks.UserRepository
            .Setup(e => e.FetchByEmailAsync(user.Email!))
            .ReturnsAsync(user);

        //Act
        var result = await handler.Handle(registerRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Authentication.EmailAlreadyInUse, result.FirstError);
        mocks.ShouldCallFetchUserByEmail(user.Email!)
            .ShouldNotCallCreateUser();
    }

    [Fact]
    public async Task Register_WhenCreateUserIsUnsuccessful_ShouldReturnError()
    {
        //Arrange
        var registerRequest = AuthenticationUtils.CreateRegisterRequest();
        var authResult = IdentityResult.Failed();
        
        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.UserRepository
            .Setup(e => e.CreateUserAsync(
                It.IsAny<User>(),
                It.IsAny<string>()))
            .ReturnsAsync(authResult);

        //Act
        var result = await handler.Handle(registerRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Authentication.RegisterError(authResult.Errors), result.FirstError);
        mocks.ShouldCallFetchUserByEmail(registerRequest.Email)
            .ShouldCallCreateUser();
    }
    
    public RegisterHandlerMocks GetMocks()
    {
        return new RegisterHandlerMocks(new Mock<IUserRepository>());
    }

    public RegisterHandler GetClass(RegisterHandlerMocks mocks)
    {
        return new RegisterHandler(mocks.UserRepository.Object);
    }
}

public class RegisterHandlerMocks
{
    public Mock<IUserRepository> UserRepository { get; set; }

    public RegisterHandlerMocks(
        Mock<IUserRepository> userRepository)
    {
        UserRepository = userRepository;
    }
    
    public void ShouldCallCreateUser()
    {
        UserRepository
            .Verify(e => e.CreateUserAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>()),
                Times.Once);
    }

    public RegisterHandlerMocks ShouldCallFetchUserByEmail(string email)
    {
        UserRepository
            .Verify(e => e
                .FetchByEmailAsync(email),
                Times.Once);

        return this;
    }

    public void ShouldNotCallCreateUser()
    {
        UserRepository
            .Verify(e => e.CreateUserAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>()),
                Times.Never);
    }
}