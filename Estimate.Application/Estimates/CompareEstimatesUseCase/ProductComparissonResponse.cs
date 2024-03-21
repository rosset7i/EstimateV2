using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.CompareEstimatesUseCase;

public class ProductComparissonResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal UnitPrice { get; init; }
    public double Quantity { get; init; }
    public decimal TotalPrice { get; init; }
    public bool IsEconomic { get; set; }

    private ProductComparissonResponse(
        Guid id,
        string name,
        decimal unitPrice,
        double quantity,
        decimal totalPrice,
        bool isEconomic)
    {
        Id = id;
        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = totalPrice;
        IsEconomic = isEconomic;
    }

    private static ProductComparissonResponse Of(ProductInEstimate productInEstimate)
    {
        return new ProductComparissonResponse(
            productInEstimate.Product.Id,
            productInEstimate.Product.Name,
            productInEstimate.Price.UnitPrice,
            productInEstimate.Price.Quantity,
            productInEstimate.Price.TotalPrice,
            false);
    }

    public static List<ProductComparissonResponse> Of(IEnumerable<ProductInEstimate> productInEstimate)
    {
        return productInEstimate
            .Select(product => Of(product))
            .ToList();
    }
}