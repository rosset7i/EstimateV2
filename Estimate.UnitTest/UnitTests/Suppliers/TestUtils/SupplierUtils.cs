using Estimate.Application.Suppliers.Dtos;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Suppliers.TestUtils;

public static class SupplierUtils
{
    public static CreateSupplierRequest CreateSupplierRequest() =>
        new(Constants.Supplier.Name);

    public static UpdateSupplierInfoRequest UpdateSupplierRequest() =>
        new(Constants.Supplier.Name);

    public static Supplier Supplier() =>
        new(Constants.Supplier.Guid,
            Constants.Supplier.Name);
}

