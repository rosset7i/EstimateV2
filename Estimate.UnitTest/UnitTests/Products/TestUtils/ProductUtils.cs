using Estimate.Application.Estimates.Dtos;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Products.Dtos;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Products.TestUtils;

public static class ProductUtils
{
    public static CreateProductRequest CreateProductRequest() =>
        new(Constants.Product.Name);

    public static UpdateProductRequest UpdateProductRequest() =>
        new(Constants.Product.Name);

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

