using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public class EstimateResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SupplierName { get; set; }

    private EstimateResponse(
        Guid id,
        string name,
        string supplierName)
    {
        Id = id;
        Name = name;
        SupplierName = supplierName;
    }

    public static EstimateResponse Of(EstimateEn estimate)
    {
        return new EstimateResponse(
            estimate.Id,
            estimate.Name,
            estimate.Supplier.Name);
    }
}