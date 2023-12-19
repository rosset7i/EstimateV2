using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierHandler
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

    public async Task CreateSupplierAsync(CreateSupplierRequest request)
    {
        var newSupplier = new Supplier(
            Guid.NewGuid(),
            request.Name);

        await _supplierRepository.AddAsync(newSupplier);
        await _unitOfWork.SaveChangesAsync();
    }
}