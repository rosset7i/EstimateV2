using Estimate.Application.Suppliers.CreateSupplierUseCase;
using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Suppliers.TestUtils;

public static class SupplierUtils
{
    public static CreateSupplierCommand CreateSupplierRequest() =>
        new(Constants.Supplier.Name);

    public static UpdateSupplierCommand UpdateSupplierRequest() =>
        new(Constants.Supplier.Guid,
            Constants.Supplier.Name);

    public static Supplier Supplier() =>
        new(Constants.Supplier.Guid,
            Constants.Supplier.Name);
}

