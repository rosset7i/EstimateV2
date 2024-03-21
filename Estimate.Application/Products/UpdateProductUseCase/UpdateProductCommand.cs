using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Products.UpdateProductUseCase;

public record UpdateProductCommand(
    Guid ProductId,
    string Name) : IRequest<ResultOf<Operation>>;