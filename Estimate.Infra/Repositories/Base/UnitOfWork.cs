using Estimate.Application.Common.Repositories.Base;
using Estimate.Infra.AppDbContext;

namespace Estimate.Infra.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly EstimateDbContext _dbContext;

    public UnitOfWork(EstimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}