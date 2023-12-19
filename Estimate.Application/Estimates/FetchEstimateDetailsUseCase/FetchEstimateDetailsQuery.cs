using MediatR;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsQuery : IRequest<FetchEstimateDetailsResponse>
{
    public Guid EstimateId { get; set; }
}