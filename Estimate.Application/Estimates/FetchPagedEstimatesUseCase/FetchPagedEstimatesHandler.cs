using Estimate.Application.Infrastructure;
using Estimate.Application.Infrastructure.Models.PagingAndSorting;
using Microsoft.EntityFrameworkCore;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public class FetchPagedEstimatesHandler
{
    private readonly IDatabaseContext _dbContext;

    public FetchPagedEstimatesHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync(PagedAndSortedEstimateRequest request)
    {
        return await _dbContext.Estimate
            .With(!string.IsNullOrEmpty(request.Name), e => e.Name.ToLower().Contains(request.Name!.ToLower()))
            .With(request.SupplierId.HasValue, e => e.SupplierId == request.SupplierId)
            .Include(e => e.Supplier)
            .SortBy(request)
            .Select(estimate => EstimateResponse.Of(estimate))
            .PageBy(request);
    }
}