using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Common;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public class UpdateEstimateProductsHandler : IRequestHandler<UpdateEstimateProductsCommand, ResultOf<Operation>>
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

    public async Task<ResultOf<Operation>> Handle(UpdateEstimateProductsCommand command, CancellationToken cancellationToken)
    {
        var estimate = await _estimateRepository.FetchEstimateWithProducts(command.EstimateId);

        var errors = Validator.New()
            .When(estimate is null, DomainError.Common.NotFound<EstimateEn>())
            .When(!await ProductsExistsAsync(command.UpdateEstimateProductsRequest), DomainError.Common.NotFound<Product>())
            .ReturnErrors();

        if (errors.Any())
            return errors;

        var productsToAdd =
            UpdateEstimateProductsRequest.ConvertToNewEntityList(
                command.UpdateEstimateProductsRequest,
                estimate!.Id);

        estimate.UpdateProducts(productsToAdd);

        await _estimateRepository.UpdateProducts(estimate);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Updated;
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