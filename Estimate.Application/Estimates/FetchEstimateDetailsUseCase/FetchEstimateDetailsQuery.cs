using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsQuery : IRequest<ResultOf<FetchEstimateDetailsResponse>>
{
    public Guid EstimateId { get; set; }
}