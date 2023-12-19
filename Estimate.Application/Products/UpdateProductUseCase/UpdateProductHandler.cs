using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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
}