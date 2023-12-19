using Estimate.Application.Suppliers.Dtos;

namespace Estimate.Application.Suppliers.Services.Interfaces;

public interface ISupplierStore
{
    Task CreateSupplierAsync(CreateSupplierRequest request);
    Task UpdateSupplierAsync(
        Guid supplierId,
        UpdateSupplierInfoRequest request);
    Task DeleteSupplierByIdAsync(Guid supplierId);
}