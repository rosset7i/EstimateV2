using Bogus;
using Estimate.Application.Estimates.CreateEstimateUseCase;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Estimates.UpdateEstimateUseCase;
using Estimate.Domain.Entities.Estimate;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Estimates.TestUtils;

public static class EstimateUtils
{
    public static CreateEstimateCommand CreateEstimateRequest() =>
        new Faker<CreateEstimateCommand>()
            .RuleFor(e => e.Name, f => f.Name.FirstName())
            .RuleFor(e => e.SupplierId, Guid.NewGuid())
            .RuleFor(e => e.ProductsInEstimate, UpdateEstimateProductsRequest())
            .Generate();

    public static UpdateEstimateCommand UpdateEstimateInfoRequest() =>
        new Faker<UpdateEstimateCommand>()
            .Generate();

    public static EstimateEn Estimate() =>
        new(Constants.Estimate.Guid,
            Constants.Estimate.Name,
            Constants.Supplier.Guid);

    public static List<UpdateEstimateProductsRequest> UpdateEstimateProductsRequest() =>
        new Faker<UpdateEstimateProductsRequest>()
            .RuleFor(e => e.Quantity, f => f.Random.Number(min:1))
            .RuleFor(e => e.UnitPrice, f => f.Random.Number(min:1))
            .Generate(3);
}

