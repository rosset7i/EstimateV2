using Estimate.Application.Common;
using MediatR;
using Rossetti.Common.Data;
using Rossetti.Common.Data.Pagination;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public class FetchPagedSuppliersHandler : IRequestHandler<PagedAndSortedSupplierQuery, PagedResultOf<SupplierResponse>>
{
    private readonly IDatabaseContext _dbContext;

    public FetchPagedSuppliersHandler(IDatabaseContext dbContext) =>
        _dbContext = dbContext;

    public async Task<PagedResultOf<SupplierResponse>> Handle(PagedAndSortedSupplierQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Supplier
            .With(!string.IsNullOrEmpty(query.Name), e => e.Name.ToLower().Contains(query.Name!.ToLower()))
            .SortBy(query)
            .Select(supplier => SupplierResponse.Of(supplier))
            .ToPagedListAsync(query, cancellationToken: cancellationToken);
    }
}