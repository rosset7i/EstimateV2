using Estimate.Application.Estimates.Dtos;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateHandler
{
    private readonly IEstimateRepository _estimateRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEstimateHandler(
        IEstimateRepository estimateRepository,
        ISupplierRepository supplierRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _estimateRepository = estimateRepository;
        _supplierRepository = supplierRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateEstimateAsync(CreateEstimateRequest request)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(request.SupplierId);

        Validator.New()
            .When(supplier is null, DomainError.Common.NotFound<Supplier>())
            .When(!await ProductsExistsAsync(request.ProductsInEstimate), DomainError.Common.NotFound<Product>())
            .ThrowExceptionIfAny();

        var newEstimate = new EstimateEn(
            Guid.NewGuid(),
            request.Name,
            request.SupplierId);

        var productsToAdd =
            UpdateEstimateProductsRequest.ConvertToNewEntityList(
                request.ProductsInEstimate,
                newEstimate.Id);

        newEstimate.UpdateProducts(productsToAdd);

        await _estimateRepository.AddAsync(newEstimate);
        await _unitOfWork.SaveChangesAsync();
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
}