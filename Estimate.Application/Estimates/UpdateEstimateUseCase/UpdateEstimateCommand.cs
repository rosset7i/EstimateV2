using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using MediatR;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

public class UpdateEstimateCommand : IRequest<ResultOf<Operation>>
{
    public Guid EstimateId { get; set; }
    public string Name { get; set; }
    public Guid SupplierId { get; set; }

    public UpdateEstimateCommand(
        string name,
        Guid supplierId)
    {
        Name = name;
        SupplierId = supplierId;
    }

    public EstimateEn UpdateInfoOf(EstimateEn estimate)
    {
        estimate.AlterName(Name);
        estimate.AlterSupplier(SupplierId);
        
        return estimate;
    }
}
    
    