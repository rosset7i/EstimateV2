namespace Estimate.Application.Common.Models.PagingAndSorting;

public record PagedAndSortedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; }
    public string Direction { get; set; } = SortDirection.Asc.ToString();
}