using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities.Estimate;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Estimate.Infra.Repositories;

public class EstimateRepository : RepositoryBase<Guid, EstimateEn>, IEstimateRepository
{
    public EstimateRepository(
        EstimateDbContext dbContext,
        IDistributedCache distributedCache) : base(dbContext, distributedCache)
    {
    }

    public async Task<EstimateEn?> FetchEstimateWithProducts(Guid estimateId) =>
        await DbContext.Set<EstimateEn>()
            .Where(e => e.Id == estimateId)
            .Include(e => e.ProductsInEstimate)
            .ThenInclude(e => e.Product)
            .FirstOrDefaultAsync();


    public async Task UpdateProducts(EstimateEn estimate) =>
        await DbContext.Set<ProductInEstimate>()
            .AddRangeAsync(estimate.ProductsInEstimate);
}