using Estimate.Application.Common;
using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Products.FetchPagedProductsUseCase;

public class FetchPagedProductsHandler : IRequestHandler<PagedAndSortedProductQuery, PagedResultOf<ProductResponse>>
{
    private readonly IDatabaseContext _dbContext;

    public FetchPagedProductsHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<ProductResponse>> Handle(PagedAndSortedProductQuery query, CancellationToken cancellationToken)
    {
        return await _dbContext.Product
            .With(!string.IsNullOrEmpty(query.Name), e => e.Name.ToLower().Contains(query.Name!.ToLower()))
            .With(query.ProductsIdsToFilter!.Any(), e => !query.ProductsIdsToFilter!.Contains(e.Id))
            .SortBy(query)
            .Select(product => ProductResponse.Of(product))
            .ToPagedListAsync(query);
    }
}