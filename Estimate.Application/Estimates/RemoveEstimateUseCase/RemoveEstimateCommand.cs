using MediatR;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public class RemoveEstimateCommand : IRequest<RemoveEstimateResult>
{
    public Guid EstimateId { get; set; }
}