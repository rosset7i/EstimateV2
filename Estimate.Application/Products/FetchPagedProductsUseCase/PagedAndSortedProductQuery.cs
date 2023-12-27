using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Products.FetchPagedProductsUseCase;

public class PagedAndSortedProductQuery : PagedAndSortedRequest, IRequest<PagedResultOf<ProductResponse>>
{
    public string? Name { get; set; }
    public List<Guid>? ProductsIdsToFilter { get; set; } = new();
}