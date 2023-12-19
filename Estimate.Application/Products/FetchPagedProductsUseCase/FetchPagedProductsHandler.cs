using Estimate.Application.Infrastructure;
using Estimate.Application.Infrastructure.Models.PagingAndSorting;
namespace Estimate.Application.Products.FetchPagedProductsUseCase;

public class FetchPagedProductsHandler
{
    private readonly IDatabaseContext _dbContext;

    public FetchPagedProductsHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync(PagedAndSortedProductRequest request)
    {
        return await _dbContext.Product
            .With(!string.IsNullOrEmpty(request.Name), e => e.Name.ToLower().Contains(request.Name!.ToLower()))
            .With(request.ProductsIdsToFilter!.Any(), e => !request.ProductsIdsToFilter!.Contains(e.Id))
            .SortBy(request)
            .Select(product => ProductResponse.Of(product))
            .PageBy(request);
    }
}