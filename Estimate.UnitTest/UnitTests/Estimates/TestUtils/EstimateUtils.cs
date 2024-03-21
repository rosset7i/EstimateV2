using Bogus;
using Estimate.Application.Common.Models;
using Estimate.Application.Estimates.CreateEstimateUseCase;
using Estimate.Application.Estimates.UpdateEstimateUseCase;
using Estimate.Domain.Entities.Estimate;

namespace Estimate.UnitTest.UnitTests.Estimates.TestUtils;

public static class EstimateUtils
{
    public static CreateEstimateCommand CreateEstimateRequest()
    {
        return new Faker<CreateEstimateCommand>()
            .CustomInstantiator(f => new CreateEstimateCommand(
                f.Name.FirstName(),
                Guid.NewGuid(),
                UpdateEstimateProductsRequest()));
    }

    public static UpdateEstimateCommand UpdateEstimateInfoRequest()
    {
        return new Faker<UpdateEstimateCommand>()
            .CustomInstantiator(f => new UpdateEstimateCommand(
                Guid.NewGuid(),
                f.Name.FirstName(),
                Guid.NewGuid()));
    }

    public static EstimateEn Estimate()
    {
        return new Faker<EstimateEn>()
            .CustomInstantiator(f => new EstimateEn(
                Guid.NewGuid(),
                f.Name.FirstName(),
                Guid.NewGuid()));
    }

    public static List<UpdateEstimateProductsRequest> UpdateEstimateProductsRequest()
    {
        return new Faker<UpdateEstimateProductsRequest>()
            .CustomInstantiator(f => new UpdateEstimateProductsRequest(
                Guid.NewGuid(),
                f.Random.Double(1),
                f.Random.Decimal(1)))
            .Generate(3);
    }
}