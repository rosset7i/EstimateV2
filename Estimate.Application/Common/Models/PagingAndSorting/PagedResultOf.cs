namespace Estimate.Application.Common.Models.PagingAndSorting;

public class PagedResultOf<TOutput>
{
    public int TotalPages { get; }
    public int TotalItems { get; }
    public int CurrentPage { get; }
    public List<TOutput> Items { get; }

    public PagedResultOf(
        int totalPages,
        int totalItems,
        int page,
        List<TOutput> items)
    {
        TotalPages = totalPages;
        TotalItems = totalItems;
        CurrentPage = page;
        Items = items;
    }
}