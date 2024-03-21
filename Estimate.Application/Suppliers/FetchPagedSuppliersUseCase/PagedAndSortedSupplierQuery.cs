using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public record PagedAndSortedSupplierQuery(string Name) : PagedAndSortedRequest, IRequest<PagedResultOf<SupplierResponse>>;