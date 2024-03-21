using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.CreateProductUseCase;

public record CreateProductCommand(string Name) : IRequest<ResultOf<Operation>>;