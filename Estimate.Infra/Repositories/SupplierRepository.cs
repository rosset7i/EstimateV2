using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace Estimate.Infra.Repositories;

public class SupplierRepository : RepositoryBase<Guid,Supplier>, ISupplierRepository
{
    public SupplierRepository(
        EstimateDbContext dbContext,
        IDistributedCache distributedCache) : base(dbContext, distributedCache)
    {
    }
}