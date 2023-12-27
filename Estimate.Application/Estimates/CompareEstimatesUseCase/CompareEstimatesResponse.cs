using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.CompareEstimatesUseCase;

public class CompareEstimatesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ProductComparissonResponse> ProductsInEstimate { get; set; }

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

public class ProductComparissonResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public double Quantity { get; set; }
    public decimal TotalPrice { get; set; }
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

    public static List<ProductComparissonResponse> Of(List<ProductInEstimate> productInEstimate)
    {
        return productInEstimate
            .Select(product => Of(product))
            .ToList();
    }
}