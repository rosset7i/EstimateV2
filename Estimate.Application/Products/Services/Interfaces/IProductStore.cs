using Estimate.Application.Products.Dtos;

namespace Estimate.Application.Products.Services.Interfaces;

public interface IProductStore
{
    Task CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(Guid productId, UpdateProductRequest request);
    Task DeleteProductAsync(Guid productId);
}