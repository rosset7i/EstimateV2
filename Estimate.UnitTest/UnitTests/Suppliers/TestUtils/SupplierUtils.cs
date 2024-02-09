using Bogus;
using Estimate.Application.Suppliers.CreateSupplierUseCase;
using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Entities;

namespace Estimate.UnitTest.UnitTests.Suppliers.TestUtils;

public static class SupplierUtils
{
    private static readonly Faker Faker = new();

    public static CreateSupplierCommand CreateSupplierRequest() =>
        new Faker<CreateSupplierCommand>()
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .Generate();

    public static UpdateSupplierCommand UpdateSupplierRequest() =>
        new Faker<UpdateSupplierCommand>()
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .Generate();

    public static Supplier Supplier() =>
        new(Guid.NewGuid(),
            Faker.Name.FirstName());
}

