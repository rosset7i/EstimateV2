using Estimate.Application.Products.Dtos;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Application.Products.Services.Interfaces;

public interface IProductQuery
{
    Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync(PagedAndSortedProductRequest request);
}