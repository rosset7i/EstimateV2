using Estimate.Domain.Entities;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public class SupplierResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    private SupplierResponse(
        Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public static SupplierResponse Of(Supplier supplier)
    {
        return new SupplierResponse(
            supplier.Id,
            supplier.Name);
    }
}