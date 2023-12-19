using AutoFixture;
using Estimate.Application.Estimates.CreateEstimateUseCase;
using Estimate.Application.Estimates.Dtos;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Estimates.UpdateEstimateUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;

namespace Estimate.UnitTest.UnitTests.Estimates.TestUtils;

public static class EstimateUtils
{
    private static readonly Fixture Fixture = new();

    public static CreateEstimateRequest CreateEstimateRequest() =>
        new(Constants.Estimate.Name,
            Constants.Supplier.Guid,
            Constants.Estimate.UpdateEstimateProductsRequests);

    public static UpdateEstimateInfoRequest UpdateEstimateInfoRequest() =>
        new(Constants.Estimate.Name,
            Constants.Supplier.Guid);

    public static EstimateEn Estimate() =>
        new(Constants.Estimate.Guid,
            Constants.Estimate.Name,
            Constants.Supplier.Guid);

    public static List<UpdateEstimateProductsRequest> UpdateEstimateProductsRequest() =>
        Fixture.CreateMany<UpdateEstimateProductsRequest>()
            .ToList();

}

