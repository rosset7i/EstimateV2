using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Application.Products.Dtos;

public class PagedAndSortedProductRequest : PagedAndSortedRequest
{
    public string? Name { get; set; }
    public List<Guid>? ProductsIdsToFilter { get; set; } = new();
}