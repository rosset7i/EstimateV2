using Estimate.Core.Suppliers.Dtos;
using Estimate.Core.Suppliers.Services.Interfaces;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Core.Suppliers.Services;

public class SupplierQuery : ISupplierQuery
{
    private readonly EstimateDbContext _dbContext;

    public SupplierQuery(EstimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<SupplierResponse>> FetchPagedSuppliersAsync(PagedAndSortedSupplierRequest request)
    {
        return await _dbContext.Set<Supplier>()
            .With(!string.IsNullOrEmpty(request.Name),e => e.Name.ToLower().Contains(request.Name!.ToLower()))
            .SortBy(request)
            .Select(supplier => SupplierResponse.Of(supplier))
            .PageBy(request);
    }
}