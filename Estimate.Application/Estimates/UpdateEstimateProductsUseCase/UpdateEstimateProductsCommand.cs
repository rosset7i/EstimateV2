using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.ValueObjects;
using MediatR;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public class UpdateEstimateProductsCommand : IRequest<ResultOf<Operation>>
{
    public Guid EstimateId { get; set; }
    public List<UpdateEstimateProductsRequest> UpdateEstimateProducts { get; set; } = new ();
}

public class UpdateEstimateProductsRequest
{
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    private ProductInEstimate ConvertToNewEntity(Guid estimateId)
    {
        return new ProductInEstimate(
            Guid.NewGuid(),
            new Price(UnitPrice, Quantity),
            ProductId,
            estimateId);
    }

    public static List<ProductInEstimate> ConvertToNewEntityList(
        List<UpdateEstimateProductsRequest> productsToAdd,
        Guid estimateId)
    {
        return productsToAdd
            .Select(e => e.ConvertToNewEntity(estimateId))
            .ToList();
    }

    public static List<Guid> ExtractProductIds(List<UpdateEstimateProductsRequest> request)
    {
        return request
            .Select(e => e.ProductId)
            .ToList();
    }
}