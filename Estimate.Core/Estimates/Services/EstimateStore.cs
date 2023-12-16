using Estimate.Core.Estimates.Dtos;
using Estimate.Core.Estimates.Services.Interfaces;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Core.Estimates.Services;

public class EstimateStore : IEstimateStore
{
    private readonly IEstimateRepository _estimateRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EstimateStore(
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
    
    public async Task UpdateEstimateInfoAsync(
        Guid estimateId,
        UpdateEstimateInfoRequest request)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(estimateId);
        var supplier = await _supplierRepository.FetchByIdAsync(request.SupplierId);

        Validator.New()
            .When(estimate is null, DomainError.Common.NotFound<EstimateEn>())
            .When(supplier is null, DomainError.Common.NotFound<Supplier>())
            .ThrowExceptionIfAny();

        UpdateEstimateInfo(estimate!, request);
        await _unitOfWork.SaveChangesAsync();
    }
    
    private void UpdateEstimateInfo(
        EstimateEn estimate,
        UpdateEstimateInfoRequest request)
    {
        var updateEstimate = request.UpdateInfoOf(estimate);
        _estimateRepository.Update(updateEstimate);
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

    public async Task DeleteEstimateByIdAsync(Guid estimateId)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(estimateId);

        if (estimate is null)
            throw new BusinessException(DomainError.Common.NotFound<EstimateEn>());

        _estimateRepository.Delete(estimate);
        await _unitOfWork.SaveChangesAsync();
    }
}