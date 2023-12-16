namespace Estimate.Core.Authentication.Dtos;

public class LoginResponse
{
    public string Email { get; set; }
    public string Token { get; set; }
    
    public LoginResponse(
        string email,
        string token)
    {
        Email = email;
        Token = token;
    }
}