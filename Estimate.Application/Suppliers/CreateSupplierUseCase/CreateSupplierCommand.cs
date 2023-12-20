using MediatR;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierCommand : IRequest<CreateSupplierResult>
{
    public string Name { get; set; }
    
    public CreateSupplierCommand(
        string name)
    {
        Name = name;
    }
}