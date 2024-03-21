using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Products.FetchPagedProductsUseCase;

public record PagedAndSortedProductQuery(
    string Name,
    List<Guid> ProductsIdsToFilter) : PagedAndSortedRequest, IRequest<PagedResultOf<ProductResponse>>;