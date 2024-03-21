using Estimate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rossetti.Common.Data;
using Rossetti.Common.Data.Pagination;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public class FetchPagedEstimatesHandler : IRequestHandler<FetchPagedEstimatesQuery, PagedResultOf<EstimateResponse>>
{
    private readonly IDatabaseContext _dbContext;

    public FetchPagedEstimatesHandler(IDatabaseContext dbContext) =>
        _dbContext = dbContext;

    public async Task<PagedResultOf<EstimateResponse>> Handle(FetchPagedEstimatesQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Estimate
            .With(!string.IsNullOrEmpty(query.Name), e => e.Name.ToLower().Contains(query.Name!.ToLower()))
            .With(query.SupplierId.HasValue, e => e.SupplierId == query.SupplierId)
            .Include(e => e.Supplier)
            .SortBy(query)
            .Select(estimate => EstimateResponse.Of(estimate))
            .ToPagedListAsync(query);
    }
}
