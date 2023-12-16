using Estimate.Domain.Entities;

namespace Estimate.Core.Products.Dtos;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    private ProductResponse(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public static ProductResponse Of(Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name);
    }
}