using Estimate.Domain.Entities;

namespace Estimate.Application.Common;

public interface IJwtTokenGeneratorService
{
    string GenerateToken(User user);
}