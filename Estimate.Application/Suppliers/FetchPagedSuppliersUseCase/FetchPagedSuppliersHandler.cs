using Estimate.Application.Infrastructure;
using Estimate.Application.Infrastructure.Models.PagingAndSorting;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public class FetchPagedSuppliersHandler
{
    private readonly IDatabaseContext _dbContext;

    public FetchPagedSuppliersHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<SupplierResponse>> FetchPagedSuppliersAsync(PagedAndSortedSupplierRequest request)
    {
        return await _dbContext.Supplier
            .With(!string.IsNullOrEmpty(request.Name),e => e.Name.ToLower().Contains(request.Name!.ToLower()))
            .SortBy(request)
            .Select(supplier => SupplierResponse.Of(supplier))
            .PageBy(request);
    }
}