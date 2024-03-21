using Estimate.Domain.Common.CommonResults;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierCommand : IRequest<ResultOf<Operation>>
{
    public string Name { get; set; }
}