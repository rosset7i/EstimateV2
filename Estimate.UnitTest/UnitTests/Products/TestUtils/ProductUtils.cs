using Bogus;
using Estimate.Application.Common.Models;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Entities;

namespace Estimate.UnitTest.UnitTests.Products.TestUtils;

public static class ProductUtils
{
    private static readonly Faker Faker = new();

    public static CreateProductCommand CreateProductRequest() =>
        new Faker<CreateProductCommand>()
            .Generate();

    public static UpdateProductCommand UpdateProductRequest() =>
        new Faker<UpdateProductCommand>()
            .Generate();

    public static Product Product() =>
        new(Guid.NewGuid(),
            Faker.Name.FirstName());

    public static List<Product> Products(List<UpdateEstimateProductsRequest> requests)
    {
        return requests
            .Select(request => new Product(
                request.ProductId,
                Faker.Name.FirstName()))
            .ToList();
    }
}

