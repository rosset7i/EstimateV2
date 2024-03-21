using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public record RemoveEstimateCommand(Guid EstimateId) : IRequest<ResultOf<Operation>>;