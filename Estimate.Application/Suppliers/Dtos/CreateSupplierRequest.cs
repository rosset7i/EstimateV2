namespace Estimate.Application.Suppliers.Dtos;

public class CreateSupplierRequest
{
    public string Name { get; set; }
    
    public CreateSupplierRequest(
        string name)
    {
        Name = name;
    }
}