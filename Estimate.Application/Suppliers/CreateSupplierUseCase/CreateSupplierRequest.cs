namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierRequest
{
    public string Name { get; set; }
    
    public CreateSupplierRequest(
        string name)
    {
        Name = name;
    }
}