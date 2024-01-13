using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierCommand : IRequest<ResultOf<Operation>>
{
    public string Name { get; set; }

    public CreateSupplierCommand(
        string name)
    {
        Name = name;
    }
}