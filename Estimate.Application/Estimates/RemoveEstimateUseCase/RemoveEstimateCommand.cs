using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public class RemoveEstimateCommand : IRequest<ResultOf<Operation>>
{
    public Guid EstimateId { get; set; }
}