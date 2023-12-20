using MediatR;

namespace Estimate.Application.Suppliers.RemoveSupplierUseCase;

public class RemoveSupplierCommand : IRequest<RemoveSupplierResult>
{
    public Guid SupplierId { get; set; }
}