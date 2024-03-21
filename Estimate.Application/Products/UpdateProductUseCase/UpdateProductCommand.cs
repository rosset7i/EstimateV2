using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.UpdateProductUseCase;

public class UpdateProductCommand : IRequest<ResultOf<Operation>>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }

    public Product UpdateInfoOf(Product product)
    {
        product.AlterName(Name);

        return product;
    }
}