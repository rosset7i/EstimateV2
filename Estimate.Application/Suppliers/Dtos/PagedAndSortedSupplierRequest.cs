using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Application.Suppliers.Dtos;

public class PagedAndSortedSupplierRequest : PagedAndSortedRequest
{
    public string? Name { get; set; }
}