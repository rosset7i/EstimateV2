using Microsoft.AspNetCore.Identity;

namespace Estimate.Domain.Entities;

public sealed class User : IdentityUser
{
    public string Name { get; set; }

    public User(
        string name,
        string email,
        string phoneNumber)
    {
        Name = name;
        Email = email;
        UserName = email;
        PhoneNumber = phoneNumber;
    }
}