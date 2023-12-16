namespace Estimate.Core.Suppliers.Dtos;

public class CreateSupplierRequest
{
    public string Name { get; set; }
    
    public CreateSupplierRequest(
        string name)
    {
        Name = name;
    }
}