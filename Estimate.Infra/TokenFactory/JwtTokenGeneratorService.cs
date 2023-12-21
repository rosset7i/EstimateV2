using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Estimate.Application.Common;
using Estimate.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Estimate.Infra.TokenFactory;

public class JwtTokenGeneratorService : IJwtTokenGeneratorService
{
    private readonly IConfiguration _configuration;

    public JwtTokenGeneratorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var key = _configuration.GetSection("JwtSettings:Secret").Value!;
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);
            
        var claims = new List<Claim>
        {
            new("email", user.Email),
            new("nome", user.Name),
        };

        var securityToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddMinutes(20),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(securityToken);
    }
}