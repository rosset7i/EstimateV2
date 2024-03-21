namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginResult
{
    public string Email { get; init; }
    public string Token { get; init; }

    public LoginResult(
        string email,
        string token)
    {
        Email = email;
        Token = token;
    }
}