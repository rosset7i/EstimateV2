using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateCommand : IRequest<ResultOf<Operation>>
{
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
    public List<UpdateEstimateProductsRequest> ProductsInEstimate { get; set; } = new();
}