using Estimate.Core.Suppliers.Dtos;

namespace Estimate.Core.Suppliers.Services.Interfaces;

public interface ISupplierStore
{
    Task CreateSupplierAsync(CreateSupplierRequest request);
    Task UpdateSupplierAsync(
        Guid supplierId,
        UpdateSupplierInfoRequest request);
    Task DeleteSupplierByIdAsync(Guid supplierId);
}