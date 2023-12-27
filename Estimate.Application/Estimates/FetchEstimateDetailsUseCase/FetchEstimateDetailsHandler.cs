using Estimate.Application.Common;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsHandler : IRequestHandler<FetchEstimateDetailsQuery, ResultOf<FetchEstimateDetailsResponse>>
{
    private readonly IDatabaseContext _dbContext;

    public FetchEstimateDetailsHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultOf<FetchEstimateDetailsResponse>> Handle(FetchEstimateDetailsQuery query, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Estimate
            .Where(e => e.Id == query.EstimateId)
            .Include(e => e.Supplier)
            .Include(e => e.ProductsInEstimate)
            .ThenInclude(p => p.Product)
            .Select(e => FetchEstimateDetailsResponse.Of(e))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result is null)
            return DomainError.Common.NotFound<EstimateEn>();

        return result;
    }
}