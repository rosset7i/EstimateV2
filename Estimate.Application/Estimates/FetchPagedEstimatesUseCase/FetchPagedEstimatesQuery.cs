using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public class FetchPagedEstimatesQuery : PagedAndSortedRequest, IRequest<PagedResultOf<EstimateResponse>>
{
    public string Name { get; set; }
    public Guid? SupplierId { get; set; }
}