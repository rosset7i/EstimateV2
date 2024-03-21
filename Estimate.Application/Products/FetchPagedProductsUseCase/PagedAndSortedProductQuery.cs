using MediatR;
using Rossetti.Common.Data.Pagination;

namespace Estimate.Application.Products.FetchPagedProductsUseCase;

public record PagedAndSortedProductQuery(
    string Name,
    List<Guid> ProductsIdsToFilter) : PagedAndSortedRequest, IRequest<PagedResultOf<ProductResponse>>;