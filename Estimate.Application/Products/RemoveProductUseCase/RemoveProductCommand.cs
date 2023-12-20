using MediatR;

namespace Estimate.Application.Products.RemoveProductUseCase;

public class RemoveProductCommand : IRequest<RemoveProductResult>
{
    public Guid ProductId { get; set; }
}