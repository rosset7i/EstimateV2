using Bogus;
using Estimate.Application.Suppliers.CreateSupplierUseCase;
using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Suppliers.TestUtils;

public static class SupplierUtils
{
    public static CreateSupplierCommand CreateSupplierRequest() =>
        new Faker<CreateSupplierCommand>()
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .Generate();

    public static UpdateSupplierCommand UpdateSupplierRequest() =>
        new Faker<UpdateSupplierCommand>()
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .Generate();

    public static Supplier Supplier() =>
        new(Constants.Supplier.Guid,
            Constants.Supplier.Name);
}

