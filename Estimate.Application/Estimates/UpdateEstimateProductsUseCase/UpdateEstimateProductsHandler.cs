using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public class UpdateEstimateProductsHandler
{
    private readonly IEstimateRepository _estimateRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEstimateProductsHandler(
        IEstimateRepository estimateRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _estimateRepository = estimateRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    private async Task<bool> ProductsExistsAsync(List<UpdateEstimateProductsRequest> request)
    {
        var productsIds = UpdateEstimateProductsRequest
            .ExtractProductIds(request);

        var products = await _productRepository
            .FetchProductsByIdsAsync(productsIds);

        return products.All(e => productsIds.Contains(e.Id))
               && products.Count == productsIds.Count;
    }
    
    public async Task UpdateEstimateProductsAsync(
        Guid estimateId,
        List<UpdateEstimateProductsRequest> request)
    {
        var estimate = await _estimateRepository.FetchEstimateWithProducts(estimateId);

        Validator.New()
            .When(estimate is null, DomainError.Common.NotFound<EstimateEn>())
            .When(!await ProductsExistsAsync(request), DomainError.Common.NotFound<Product>())
            .ThrowExceptionIfAny();

        var productsToAdd =
            UpdateEstimateProductsRequest.ConvertToNewEntityList(
                request,
                estimate!.Id);

        estimate.UpdateProducts(productsToAdd);

        await _estimateRepository.UpdateProducts(estimate);
        await _unitOfWork.SaveChangesAsync();
    }
}