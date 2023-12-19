using Estimate.Application.Infrastructure;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.FetchEstimateDetailsUseCase;

public class FetchEstimateDetailsHandler
{
    private readonly IDatabaseContext _dbContext;

    public FetchEstimateDetailsHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EstimateDetailsResponse> FetchEstimateDetailsByIdAsync(Guid estimateId)
    {
        var result = await _dbContext.Estimate
            .Where(e => e.Id == estimateId)
            .Include(e => e.Supplier)
            .Include(e => e.ProductsInEstimate)
            .ThenInclude(p => p.Product)
            .Select(e => EstimateDetailsResponse.Of(e))
            .FirstOrDefaultAsync();

        if (result is null)
            throw new BusinessException(DomainError.Common.NotFound<EstimateEn>());

        return result;
    }
}