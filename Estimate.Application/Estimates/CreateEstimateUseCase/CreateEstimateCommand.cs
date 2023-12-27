using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Estimates.CreateEstimateUseCase;

public class CreateEstimateCommand : IRequest<ResultOf<Operation>>
{
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
    public List<UpdateEstimateProductsRequest> ProductsInEstimate { get; set; }
    
    public CreateEstimateCommand(
        string name,
        Guid supplierId,
        List<UpdateEstimateProductsRequest> productsInEstimate)
    {
        Name = name;
        SupplierId = supplierId;
        ProductsInEstimate = productsInEstimate;
    }
}