using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class ProductInEstimateResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal UnitPrice { get; init; }
    public double Quantity { get; init; }

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

    public static List<ProductInEstimateResponse> Of(IEnumerable<ProductInEstimate> productInEstimate)
    {
        return productInEstimate
            .Select(product => Of(product))
            .ToList();
    }
}