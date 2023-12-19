using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierHandler
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSupplierHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateSupplierAsync(
        Guid supplierId,
        UpdateSupplierInfoRequest request)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(supplierId);

        if (supplier is null)
            throw new BusinessException(DomainError.Common.NotFound<Supplier>());

        var updatedSupplier = request.UpdateInfoOf(supplier);

        _supplierRepository.Update(updatedSupplier);
        await _unitOfWork.SaveChangesAsync();
    }
}