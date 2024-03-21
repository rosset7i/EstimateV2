using Estimate.Application.Common.Models;
using Estimate.Domain.Entities.Estimate;
using Estimate.Domain.Entities.ValueObjects;

namespace Estimate.Application.Common.Helpers;

public static class CreateProductEstimateHelper
{
    public static List<ProductInEstimate> CreateProductEstimateList(
        IEnumerable<UpdateEstimateProductsRequest> productsToAdd,
        Guid estimateId)
    {
        return productsToAdd
            .Select(e => CreateProductEstimate(estimateId, e))
            .ToList();
    }

    private static ProductInEstimate CreateProductEstimate(Guid estimateId, UpdateEstimateProductsRequest request)
    {
        return new ProductInEstimate(
            Guid.NewGuid(),
            new Price(request.UnitPrice, request.Quantity),
            request.ProductId,
            estimateId);
    }

    public static List<Guid> ExtractProductIds(IEnumerable<UpdateEstimateProductsRequest> request)
    {
        return request
            .Select(e => e.ProductId)
            .ToList();
    }
}