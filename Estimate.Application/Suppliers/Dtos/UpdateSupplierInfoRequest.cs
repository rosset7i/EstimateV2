using Estimate.Domain.Entities;

namespace Estimate.Application.Suppliers.Dtos;

public class UpdateSupplierInfoRequest
{
    public string Name { get; set; }
    
    public UpdateSupplierInfoRequest(
        string name)
    {
        Name = name;
    }

    public Supplier UpdateInfoOf(Supplier supplier)
    {
        supplier.AlterName(Name);

        return supplier;
    }
}