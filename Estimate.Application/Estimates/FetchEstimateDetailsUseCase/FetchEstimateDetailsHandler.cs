using Estimate.Application.Infrastructure;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsHandler : IRequestHandler<FetchEstimateDetailsQuery, FetchEstimateDetailsResponse>
{
    private readonly IDatabaseContext _dbContext;

    public FetchEstimateDetailsHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FetchEstimateDetailsResponse> Handle(FetchEstimateDetailsQuery query, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Estimate
            .Where(e => e.Id == query.EstimateId)
            .Include(e => e.Supplier)
            .Include(e => e.ProductsInEstimate)
            .ThenInclude(p => p.Product)
            .Select(e => FetchEstimateDetailsResponse.Of(e))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (result is null)
            throw new BusinessException(DomainError.Common.NotFound<EstimateEn>());

        return result;
    }
}