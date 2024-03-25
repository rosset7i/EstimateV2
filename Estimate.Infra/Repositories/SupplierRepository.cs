using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Microsoft.Extensions.Caching.Distributed;
using Rossetti.Common.Data.Repository;

namespace Estimate.Infra.Repositories;

public class SupplierRepository : RepositoryBase<Guid, Supplier>, ISupplierRepository
{
    public SupplierRepository(
        EstimateDbContext dbContext,
        IDistributedCache distributedCache
    ) : base(dbContext, distributedCache) { }
}