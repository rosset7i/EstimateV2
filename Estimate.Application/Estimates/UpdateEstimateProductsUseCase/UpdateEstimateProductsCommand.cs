using Estimate.Application.Common.Models;
using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.UpdateEstimateProductsUseCase;

public record UpdateEstimateProductsCommand(
    Guid EstimateId,
    List<UpdateEstimateProductsRequest> UpdateEstimateProductsRequest) : IRequest<ResultOf<Operation>>;