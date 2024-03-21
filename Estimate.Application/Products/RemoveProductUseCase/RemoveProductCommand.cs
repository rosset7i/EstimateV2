using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.RemoveProductUseCase;

public class RemoveProductCommand : IRequest<ResultOf<Operation>>
{
    public Guid ProductId { get; set; }
}