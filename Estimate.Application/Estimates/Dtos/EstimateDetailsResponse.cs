using Estimate.Domain.Entities;

namespace Estimate.Application.Estimates.Dtos;

public class EstimateDetailsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<ProductInEstimateResponse> ProductsInEstimate { get; set; }

    private EstimateDetailsResponse(
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
    
    public static EstimateDetailsResponse Of(EstimateEn estimate)
    {
        return new EstimateDetailsResponse(
            estimate.Id,
            estimate.Name,
            estimate.Supplier.Id,
            estimate.Supplier.Name,
            ProductInEstimateResponse.Of(estimate.ProductsInEstimate));
    }
}