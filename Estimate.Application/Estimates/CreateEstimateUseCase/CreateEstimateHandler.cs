using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateHandler : IRequestHandler<CreateEstimateCommand, CreateEstimateResult>
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

    public async Task<CreateEstimateResult> Handle(CreateEstimateCommand command, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(command.SupplierId);

        Validator.New()
            .When(supplier is null, DomainError.Common.NotFound<Supplier>())
            .When(!await ProductsExistsAsync(command.ProductsInEstimate), DomainError.Common.NotFound<Product>())
            .ThrowExceptionIfAny();

        var newEstimate = new EstimateEn(
            Guid.NewGuid(),
            command.Name,
            command.SupplierId);

        var productsToAdd =
            UpdateEstimateProductsRequest.ConvertToNewEntityList(
                command.ProductsInEstimate,
                newEstimate.Id);

        newEstimate.UpdateProducts(productsToAdd);

        await _estimateRepository.AddAsync(newEstimate);
        await _unitOfWork.SaveChangesAsync();

        return new CreateEstimateResult();
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
