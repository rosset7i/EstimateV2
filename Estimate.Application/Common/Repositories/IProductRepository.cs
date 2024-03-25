using Estimate.Domain.Entities;
using Rossetti.Common.Data.Repository;

namespace Estimate.Application.Common.Repositories;

public interface IProductRepository : IRepositoryBase<Guid, Product>
{
    Task<List<Product>> FetchProductsByIdsAsync(List<Guid> productIds);
}