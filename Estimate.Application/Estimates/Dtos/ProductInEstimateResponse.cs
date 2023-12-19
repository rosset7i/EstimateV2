using Estimate.Domain.Entities;

namespace Estimate.Application.Estimates.Dtos;

public record ProductInEstimateResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public double Quantity { get; set; }

    private ProductInEstimateResponse(
        Guid id,
        string name,
        decimal unitPrice,
        double quantity)
    {
        Id = id;
        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    private static ProductInEstimateResponse Of(ProductInEstimate productInEstimate)
    {
        return new ProductInEstimateResponse(
            productInEstimate.Product.Id,
            productInEstimate.Product.Name,
            productInEstimate.Price.UnitPrice,
            productInEstimate.Price.Quantity);
    }
    
    public static List<ProductInEstimateResponse> Of(List<ProductInEstimate> productInEstimate)
    {
        return productInEstimate
            .Select(product => Of(product))
            .ToList();
    }
}