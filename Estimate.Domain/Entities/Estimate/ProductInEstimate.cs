using Estimate.Domain.Entities.Base;
using Estimate.Domain.Entities.ValueObjects;

namespace Estimate.Domain.Entities.Estimate;

public class ProductInEstimate : Entity
{
    public Price Price { get; }

    public Guid ProductId { get; }
    public Product Product { get; }

    public Guid EstimateId { get; }
    public EstimateEn Estimate { get; }

    public ProductInEstimate()
    {

    }

    public ProductInEstimate(
        Guid id,
        Price price,
        Guid productId,
        Guid estimateId)
    {
        Id = id;
        Price = price;
        ProductId = productId;
        EstimateId = estimateId;
    }
}