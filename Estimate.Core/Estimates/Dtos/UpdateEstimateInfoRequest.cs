using Estimate.Domain.Entities;

namespace Estimate.Core.Estimates.Dtos;

public class UpdateEstimateInfoRequest
{
    public string Name { get; set; }
    public Guid SupplierId { get; set; }

    public UpdateEstimateInfoRequest(
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
    
    