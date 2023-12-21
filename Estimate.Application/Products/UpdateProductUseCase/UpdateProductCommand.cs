using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductCommand : IRequest<ResultOf<Operation>>
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