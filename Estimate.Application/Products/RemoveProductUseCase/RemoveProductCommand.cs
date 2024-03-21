using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.RemoveProductUseCase;

public record RemoveProductCommand(Guid ProductId) : IRequest<ResultOf<Operation>>;