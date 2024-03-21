using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Entities;
using MediatR;
using Rossetti.Common.Result;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, ResultOf<Operation>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSupplierHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultOf<Operation>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        var newSupplier = new Supplier(
            Guid.NewGuid(),
            command.Name);

        await _supplierRepository.AddAsync(newSupplier);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Created;
    }
}