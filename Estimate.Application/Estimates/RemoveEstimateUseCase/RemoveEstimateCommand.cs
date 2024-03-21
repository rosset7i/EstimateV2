using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public class RemoveEstimateCommand : IRequest<ResultOf<Operation>>
{
    public Guid EstimateId { get; set; }
}