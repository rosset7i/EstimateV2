using Estimate.Domain.Entities;
using MediatR;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

public class UpdateEstimateCommand : IRequest<UpdateEstimateResult>
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
    
    