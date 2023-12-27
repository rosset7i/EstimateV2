using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities;

namespace Estimate.Application.Common.Repositories;

public interface IProductRepository : IRepositoryBase<Guid, Product>
{
    Task<List<Product>> FetchProductsByIdsAsync(List<Guid> productIds);
}