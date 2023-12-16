using Estimate.Core.Products.Dtos;

namespace Estimate.Core.Products.Services.Interfaces;

public interface IProductStore
{
    Task CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(Guid productId, UpdateProductRequest request);
    Task DeleteProductAsync(Guid productId);
}