using Estimate.Domain.Interface.Base;
using Estimate.Infra.AppDbContext;

namespace Estimate.Infra.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly EstimateDbContext _dbContext;

    public UnitOfWork(EstimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}