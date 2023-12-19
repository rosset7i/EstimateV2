using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Products.RemoveProductUseCase;

public class RemoveProductHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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