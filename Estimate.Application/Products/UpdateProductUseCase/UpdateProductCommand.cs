using Estimate.Domain.Entities;
using MediatR;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    
    public UpdateProductCommand(
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