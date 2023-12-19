using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateRequest
{
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
    public List<UpdateEstimateProductsRequest> ProductsInEstimate { get; set; }
    
    public CreateEstimateRequest(
        string name,
        Guid supplierId,
        List<UpdateEstimateProductsRequest> productsInEstimate)
    {
        Name = name;
        SupplierId = supplierId;
        ProductsInEstimate = productsInEstimate;
    }
}