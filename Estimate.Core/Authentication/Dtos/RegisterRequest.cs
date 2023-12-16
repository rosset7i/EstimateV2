namespace Estimate.Core.Authentication.Dtos;

public class RegisterRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }

    public RegisterRequest() { }

    public RegisterRequest(
        string name,
        string email,
        string password,
        string phone)
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
    }
}