namespace Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

public class PagedAndSortedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public string Direction { get; set; } = SortDirection.ASC.ToString();
}