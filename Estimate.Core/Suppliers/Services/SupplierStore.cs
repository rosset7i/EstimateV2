using Estimate.Core.Suppliers.Dtos;
using Estimate.Core.Suppliers.Services.Interfaces;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Core.Suppliers.Services;

public class SupplierStore : ISupplierStore
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SupplierStore(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateSupplierAsync(CreateSupplierRequest request)
    {
        var newSupplier = new Supplier(
            Guid.NewGuid(),
            request.Name);

        await _supplierRepository.AddAsync(newSupplier);
        await _unitOfWork.SaveChangesAsync();
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

    public async Task DeleteSupplierByIdAsync(Guid supplierId)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(supplierId);

        if (supplier is null)
            throw new BusinessException(DomainError.Common.NotFound<Supplier>());

        _supplierRepository.Delete(supplier);
        await _unitOfWork.SaveChangesAsync();
    }
}