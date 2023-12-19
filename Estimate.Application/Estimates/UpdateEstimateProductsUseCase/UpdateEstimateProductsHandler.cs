using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public class UpdateEstimateProductsHandler : IRequestHandler<UpdateEstimateProductsCommand, UpdateEstimateProductsResult>
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

    public async Task<UpdateEstimateProductsResult> Handle(UpdateEstimateProductsCommand command, CancellationToken cancellationToken)
    {
        var estimate = await _estimateRepository.FetchEstimateWithProducts(command.EstimateId);

        Validator.New()
            .When(estimate is null, DomainError.Common.NotFound<EstimateEn>())
            .When(!await ProductsExistsAsync(command.UpdateEstimateProducts), DomainError.Common.NotFound<Product>())
            .ThrowExceptionIfAny();

        var productsToAdd =
            UpdateEstimateProductsRequest.ConvertToNewEntityList(
                command.UpdateEstimateProducts,
                estimate!.Id);

        estimate.UpdateProducts(productsToAdd);

        await _estimateRepository.UpdateProducts(estimate);
        await _unitOfWork.SaveChangesAsync();

        return new UpdateEstimateProductsResult();
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