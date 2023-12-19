using Estimate.Domain.Entities;

namespace Estimate.Application.Products.UpdateProductUseCase;

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