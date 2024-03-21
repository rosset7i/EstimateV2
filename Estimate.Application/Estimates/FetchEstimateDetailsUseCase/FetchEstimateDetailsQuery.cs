using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public record FetchEstimateDetailsQuery(Guid EstimateId) : IRequest<ResultOf<FetchEstimateDetailsResponse>>;