using Estimate.Application.Products.Dtos;
using Estimate.Application.Products.Services.Interfaces;
using Estimate.Domain.Entities;
using Estimate.Infra.AppDbContext;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Application.Products.Services;

public class ProductQuery : IProductQuery
{
    private readonly EstimateDbContext _dbContext;

    public ProductQuery(EstimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync(PagedAndSortedProductRequest request)
    {
        return await _dbContext.Set<Product>()
            .With(!string.IsNullOrEmpty(request.Name), e => e.Name.ToLower().Contains(request.Name!.ToLower()))
            .With(request.ProductsIdsToFilter!.Any(), e => !request.ProductsIdsToFilter!.Contains(e.Id))
            .SortBy(request)
            .Select(product => ProductResponse.Of(product))
            .PageBy(request);
    }
}