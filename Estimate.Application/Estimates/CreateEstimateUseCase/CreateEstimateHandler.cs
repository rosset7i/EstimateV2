using Estimate.Application.Common.Helpers;
using Estimate.Application.Common.Models;
using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using MediatR;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Rossetti.Common.Validation;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateHandler : IRequestHandler<CreateEstimateCommand, ResultOf<Operation>>
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

    public async Task<ResultOf<Operation>> Handle(CreateEstimateCommand command, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(command.SupplierId);

        var errors = Validator.New()
            .When(supplier is null, CommonError.NotFound<Supplier>())
            .When(!await ProductsExistsAsync(command.ProductsInEstimate), CommonError.NotFound<Product>())
            .ReturnErrors();

        if (errors.Any())
            return errors;

        var newEstimate = new EstimateEn(
            Guid.NewGuid(),
            command.Name,
            command.SupplierId);

        var productsToAdd = CreateProductEstimateHelper.CreateProductEstimateList(
            command.ProductsInEstimate,
            newEstimate.Id);

        newEstimate.UpdateProducts(productsToAdd);

        await _estimateRepository.AddAsync(newEstimate);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Created;
    }

    private async Task<bool> ProductsExistsAsync(IEnumerable<UpdateEstimateProductsRequest> request)
    {
        var productsIds = CreateProductEstimateHelper.ExtractProductIds(request);

        var products = await _productRepository.FetchProductsByIdsAsync(productsIds);

        return products.All(e => productsIds.Contains(e.Id))
               && products.Count == productsIds.Count;
    }
}
