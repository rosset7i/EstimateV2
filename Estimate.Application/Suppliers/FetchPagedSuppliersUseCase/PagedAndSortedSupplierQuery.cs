using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public class PagedAndSortedSupplierQuery : PagedAndSortedRequest, IRequest<PagedResultOf<SupplierResponse>>
{
    public string? Name { get; set; }
}