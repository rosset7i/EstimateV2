using MediatR;
using Rossetti.Common.Data.Pagination;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public record PagedAndSortedSupplierQuery(string Name) : PagedAndSortedRequest, IRequest<PagedResultOf<SupplierResponse>>;