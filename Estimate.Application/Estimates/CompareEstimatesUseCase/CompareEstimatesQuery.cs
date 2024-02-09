using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Estimates.CompareEstimatesUseCase;

public class CompareEstimatesQuery : IRequest<ResultOf<List<CompareEstimatesResponse>>>
{
    public List<Guid> EstimateIds { get; set; } = new();
}