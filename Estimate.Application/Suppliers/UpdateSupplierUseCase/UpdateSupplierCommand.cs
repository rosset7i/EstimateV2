using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierCommand : IRequest<ResultOf<Operation>>
{
    public Guid SupplierId { get; set; }
    public UpdateSupplierInfoRequest UpdateSupplierInfoRequest { get; set; }
}