using Estimate.Application.Infrastructure.Models.PagingAndSorting;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public class PagedAndSortedEstimateRequest : PagedAndSortedRequest
{
    public string? Name { get; set; }
    public Guid? SupplierId { get; set; }
}