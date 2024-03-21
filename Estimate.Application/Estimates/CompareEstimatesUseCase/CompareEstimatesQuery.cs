using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.CompareEstimatesUseCase;

public record CompareEstimatesQuery(List<Guid> EstimateIds) : IRequest<ResultOf<List<CompareEstimatesResponse>>>;