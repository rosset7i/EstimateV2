using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Suppliers.RemoveSupplierUseCase;

public class RemoveSupplierHandler : IRequestHandler<RemoveSupplierCommand, RemoveSupplierResult>
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

    public async Task<RemoveSupplierResult> Handle(RemoveSupplierCommand command, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(command.SupplierId);

        if (supplier is null)
            throw new BusinessException(DomainError.Common.NotFound<Supplier>());

        _supplierRepository.Delete(supplier);
        await _unitOfWork.SaveChangesAsync();

        return new RemoveSupplierResult();
    }
}