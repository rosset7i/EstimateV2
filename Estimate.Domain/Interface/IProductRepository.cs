using Estimate.Domain.Entities;
using Estimate.Domain.Interface.Base;

namespace Estimate.Domain.Interface;

public interface IProductRepository : IRepositoryBase<Guid, Product>
{
    Task<List<Product>> FetchProductsByIdsAsync(List<Guid> productIds);
}