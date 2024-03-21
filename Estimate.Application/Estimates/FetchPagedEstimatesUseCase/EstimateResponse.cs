using Estimate.Domain.Entities.Estimate;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public class EstimateResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string SupplierName { get; init; }

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