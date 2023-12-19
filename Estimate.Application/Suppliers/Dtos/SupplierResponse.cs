using Estimate.Domain.Entities;

namespace Estimate.Application.Suppliers.Dtos;

public class SupplierResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

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