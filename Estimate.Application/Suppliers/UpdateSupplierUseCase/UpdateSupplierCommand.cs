using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierCommand : IRequest<ResultOf<Operation>>
{
    public Guid SupplierId { get; set; }
    public string Name { get; set; }

    public UpdateSupplierCommand(
        Guid supplierId,
        string name)
    {
        SupplierId = supplierId;
        Name = name;
    }

    public Supplier UpdateInfoOf(Supplier supplier)
    {
        supplier.AlterName(Name);

        return supplier;
    }
}