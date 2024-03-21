using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductCommand : IRequest<ResultOf<Operation>>
{
    public string Name { get; set; }
}