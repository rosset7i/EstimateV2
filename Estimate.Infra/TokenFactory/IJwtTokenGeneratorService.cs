using Estimate.Domain.Entities;

namespace Estimate.Infra.TokenFactory;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(User user);
}