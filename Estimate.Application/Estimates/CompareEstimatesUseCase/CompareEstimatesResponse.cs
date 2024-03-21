using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.CompareEstimatesUseCase;

public class CompareEstimatesResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<ProductComparissonResponse> ProductsInEstimate { get; init; }

    private CompareEstimatesResponse(
        Guid id,
        string name,
        List<ProductComparissonResponse> productsInEstimate)
    {
        Id = id;
        Name = name;
        ProductsInEstimate = productsInEstimate;
    }

    public static CompareEstimatesResponse Of(EstimateEn estimateEn)
    {
        return new CompareEstimatesResponse(
            estimateEn.Id,
            estimateEn.Name,
            ProductComparissonResponse.Of(estimateEn.ProductsInEstimate));
    }
}