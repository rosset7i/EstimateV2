using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

public record UpdateEstimateCommand(
    Guid EstimateId,
    string Name,
    Guid SupplierId) : IRequest<ResultOf<Operation>>;