using Estimate.Application.Infrastructure.Models.PagingAndSorting;

namespace Estimate.Application.Products.FetchPagedProductsUseCase;

public class PagedAndSortedProductRequest : PagedAndSortedRequest
{
    public string? Name { get; set; }
    public List<Guid>? ProductsIdsToFilter { get; set; } = new();
}