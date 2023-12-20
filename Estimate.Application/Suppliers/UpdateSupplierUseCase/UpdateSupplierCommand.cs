using MediatR;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierCommand : IRequest<UpdateSupplierResult>
{
    public Guid SupplierId { get; set; }
    public UpdateSupplierInfoRequest UpdateSupplierInfoRequest { get; set; }
}