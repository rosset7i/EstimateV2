using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid SupplierId { get; init; }
    public string SupplierName { get; init; }
    public List<ProductInEstimateResponse> ProductsInEstimate { get; init; }

    private FetchEstimateDetailsResponse(
        Guid id,
        string name,
        Guid supplierId,
        string supplierName,
        List<ProductInEstimateResponse> productsInEstimate)
    {
        Id = id;
        Name = name;
        SupplierId = supplierId;
        SupplierName = supplierName;
        ProductsInEstimate = productsInEstimate;
    }

    public static FetchEstimateDetailsResponse Of(EstimateEn estimate)
    {
        return new FetchEstimateDetailsResponse(
            estimate.Id,
            estimate.Name,
            estimate.Supplier.Id,
            estimate.Supplier.Name,
            ProductInEstimateResponse.Of(estimate.ProductsInEstimate));
    }
}