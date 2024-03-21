using Bogus;
using Estimate.Application.Common.Models;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Entities;

namespace Estimate.UnitTest.UnitTests.Products.TestUtils;

public static class ProductUtils
{
    private static readonly Faker Faker = new();

    public static CreateProductCommand CreateProductRequest()
    {
        return new Faker<CreateProductCommand>()
            .CustomInstantiator(f => new CreateProductCommand(
                f.Name.FirstName()));
    }

    public static UpdateProductCommand UpdateProductRequest()
    {
        return new Faker<UpdateProductCommand>()
            .CustomInstantiator(f => new UpdateProductCommand(
                Guid.NewGuid(),
                f.Name.FirstName()));
    }

    public static Product Product()
    {
        return new Faker<Product>()
            .CustomInstantiator(f => new Product(
                Guid.NewGuid(),
                f.Name.FirstName()));
    }

    public static List<Product> Products(IEnumerable<UpdateEstimateProductsRequest> requests)
    {
        return requests
            .Select(request => new Product(
                request.ProductId,
                Faker.Name.FirstName()))
            .ToList();
    }
}