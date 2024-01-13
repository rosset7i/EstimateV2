namespace Estimate.Application.Authentication.LoginUseCase;

public class LoginResult
{
    public string Email { get; set; }
    public string Token { get; set; }

    public LoginResult(
        string email,
        string token)
    {
        Email = email;
        Token = token;
    }
}