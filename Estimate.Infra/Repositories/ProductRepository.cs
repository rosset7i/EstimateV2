using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Estimate.Infra.Repositories;

public class ProductRepository : RepositoryBase<Guid, Product>, IProductRepository
{
    public ProductRepository(
        EstimateDbContext dbContext,
        IDistributedCache distributedCache
    ) : base(dbContext, distributedCache) { }

    public async Task<List<Product>> FetchProductsByIdsAsync(List<Guid> productIds)
    {
        return await DbContext.Set<Product>()
            .Where(e => productIds.Contains(e.Id))
            .ToListAsync();
    }
}