using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Products.CreateProductUseCase;

public class CreateProductCommand : IRequest<ResultOf<Operation>>
{
    public string Name { get; set; }
}