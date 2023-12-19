using Estimate.Application.Infrastructure.Models.PagingAndSorting;

namespace Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;

public class PagedAndSortedSupplierRequest : PagedAndSortedRequest
{
    public string? Name { get; set; }
}