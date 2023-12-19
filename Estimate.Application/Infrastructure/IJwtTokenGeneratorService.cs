using Estimate.Domain.Entities;

namespace Estimate.Application.Infrastructure;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(User user);
}