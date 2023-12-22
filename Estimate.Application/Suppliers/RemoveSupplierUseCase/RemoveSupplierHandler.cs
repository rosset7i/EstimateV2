using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Suppliers.RemoveSupplierUseCase;

public class RemoveSupplierHandler : IRequestHandler<RemoveSupplierCommand, ResultOf<Operation>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSupplierHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultOf<Operation>> Handle(RemoveSupplierCommand command, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(command.SupplierId);

        if (supplier is null)
            return DomainError.Common.NotFound<Supplier>();

        _supplierRepository.Delete(supplier);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Deleted;
    }
}