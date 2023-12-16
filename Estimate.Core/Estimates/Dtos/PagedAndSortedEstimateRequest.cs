using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Core.Estimates.Dtos;

public class PagedAndSortedEstimateRequest : PagedAndSortedRequest
{
    public string? Name { get; set; }
    public Guid? SupplierId { get; set; }
}