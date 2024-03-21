using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Entities.Estimate;
using Estimate.Domain.Entities.ValueObjects;

namespace Estimate.Application.Common.CommonServices;

public static class CreateProductEstimateService
{
    private static ProductInEstimate[] CreateProductEstimateList(
        IEnumerable<UpdateEstimateProductsRequest> productsToAdd,
        Guid estimateId)
    {
        return productsToAdd
            .Select(e => CreateProductEstimate(estimateId, e))
            .ToArray();
    }

    private static ProductInEstimate CreateProductEstimate(Guid estimateId, UpdateEstimateProductsRequest request)
    {
        return new ProductInEstimate(
            Guid.NewGuid(),
            new Price(request.UnitPrice, request.Quantity),
            request.ProductId,
            estimateId);
    }

    private static List<Guid> ExtractProductIds(IEnumerable<UpdateEstimateProductsRequest> request)
    {
        return request
            .Select(e => e.ProductId)
            .ToList();
    }
}