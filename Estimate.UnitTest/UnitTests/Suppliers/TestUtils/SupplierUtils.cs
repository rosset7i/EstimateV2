using Bogus;
using Estimate.Application.Suppliers.CreateSupplierUseCase;
using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Entities;

namespace Estimate.UnitTest.UnitTests.Suppliers.TestUtils;

public static class SupplierUtils
{
    public static CreateSupplierCommand CreateSupplierRequest()
    {
        return new Faker<CreateSupplierCommand>()
            .CustomInstantiator(f => new CreateSupplierCommand(
                f.Name.FirstName()));
    }

    public static UpdateSupplierCommand UpdateSupplierRequest()
    {
        return new Faker<UpdateSupplierCommand>()
            .CustomInstantiator(f => new UpdateSupplierCommand(
                Guid.NewGuid(),
                f.Name.FirstName()));
    }

    public static Supplier Supplier()
    {
        return new Faker<Supplier>()
            .CustomInstantiator(f => new Supplier(
                Guid.NewGuid(),
                f.Name.FirstName()));
    }
}

