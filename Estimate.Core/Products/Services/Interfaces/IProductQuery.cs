using Estimate.Core.Products.Dtos;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Core.Products.Services.Interfaces;

public interface IProductQuery
{
    Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync(PagedAndSortedProductRequest request);
}