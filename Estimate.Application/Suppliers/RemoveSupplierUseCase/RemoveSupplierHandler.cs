using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Suppliers.RemoveSupplierUseCase;

public class RemoveSupplierHandler
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

    public async Task DeleteSupplierByIdAsync(Guid supplierId)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(supplierId);

        if (supplier is null)
            throw new BusinessException(DomainError.Common.NotFound<Supplier>());

        _supplierRepository.Delete(supplier);
        await _unitOfWork.SaveChangesAsync();
    }
}