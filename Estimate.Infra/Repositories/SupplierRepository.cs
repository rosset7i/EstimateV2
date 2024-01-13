using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace Estimate.Infra.Repositories;

public class SupplierRepository : RepositoryBase<Guid, Supplier>, ISupplierRepository
{
    public SupplierRepository(
        EstimateDbContext dbContext,
        IDistributedCache distributedCache) : base(dbContext, distributedCache)
    {
    }
}