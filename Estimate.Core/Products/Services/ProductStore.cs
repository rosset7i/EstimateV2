using Estimate.Core.Products.Dtos;
using Estimate.Core.Products.Services.Interfaces;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Core.Products.Services;

public class ProductStore : IProductStore
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductStore(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateProductAsync(CreateProductRequest request)
    {
        var newProduct = new Product(
            Guid.NewGuid(),
            request.Name);

        await _productRepository.AddAsync(newProduct);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(
        Guid productId,
        UpdateProductRequest request)
    {
        var product = await _productRepository.FetchByIdAsync(productId);

        if (product is null)
            throw new BusinessException(DomainError.Common.NotFound<Product>());

        var updatedProduct = request.UpdateInfoOf(product);

        _productRepository.Update(updatedProduct);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await _productRepository.FetchByIdAsync(productId);

        if (product is null)
            throw new BusinessException(DomainError.Common.NotFound<Product>());

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();
    }
}