using Estimate.Application.Common.Models;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public record CreateEstimateCommand(
    string Name,
    Guid SupplierId,
    List<UpdateEstimateProductsRequest> ProductsInEstimate) : IRequest<ResultOf<Operation>>;