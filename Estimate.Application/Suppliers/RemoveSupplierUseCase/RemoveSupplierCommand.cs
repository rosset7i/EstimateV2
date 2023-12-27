using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Suppliers.RemoveSupplierUseCase;

public class RemoveSupplierCommand : IRequest<ResultOf<Operation>>
{
    public Guid SupplierId { get; set; }
}