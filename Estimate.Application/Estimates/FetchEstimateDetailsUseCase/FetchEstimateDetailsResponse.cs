using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<ProductInEstimateResponse> ProductsInEstimate { get; set; }

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