using Estimate.Domain.Entities;

namespace Estimate.Core.Products.Dtos;

public class UpdateProductRequest
{
    public string Name { get; set; }
    
    public UpdateProductRequest(
        string name)
    {
        Name = name;
    }

    public Product UpdateInfoOf(Product product)
    {
        product.AlterName(Name);

        return product;
    }
}