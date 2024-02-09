using Bogus;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Products.TestUtils;

public static class ProductUtils
{
    public static CreateProductCommand CreateProductRequest() =>
        new Faker<CreateProductCommand>()
            .Generate();

    public static UpdateProductCommand UpdateProductRequest() =>
        new Faker<UpdateProductCommand>()
            .Generate();

    public static Product Product() =>
        new(Constants.Product.Guid,
            Constants.Product.Name);

    public static List<Product> Products(List<UpdateEstimateProductsRequest> requests)
    {
        return requests
            .Select(request => new Product(
                request.ProductId,
                Constants.Product.Name))
            .ToList();
    }
}

