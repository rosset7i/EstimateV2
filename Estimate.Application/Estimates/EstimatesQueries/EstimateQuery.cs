using Estimate.Application.Estimates.Dtos;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;
using Microsoft.EntityFrameworkCore;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.Services;

public class EstimateQuery
{
    private readonly EstimateDbContext _dbContext;

    public EstimateQuery(EstimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync(PagedAndSortedEstimateRequest request)
    {
        return await _dbContext.Set<EstimateEn>()
            .With(!string.IsNullOrEmpty(request.Name), e => e.Name.ToLower().Contains(request.Name!.ToLower()))
            .With(request.SupplierId.HasValue, e => e.SupplierId == request.SupplierId)
            .Include(e => e.Supplier)
            .SortBy(request)
            .Select(estimate => EstimateResponse.Of(estimate))
            .PageBy(request);
    }

    public async Task<EstimateDetailsResponse> FetchEstimateDetailsByIdAsync(Guid estimateId)
    {
        var result = await _dbContext.Set<EstimateEn>()
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