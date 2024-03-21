using Estimate.Application.Common.Models.PagingAndSorting;
using MediatR;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public record FetchPagedEstimatesQuery(
    string Name,
    Guid? SupplierId) : PagedAndSortedRequest, IRequest<PagedResultOf<EstimateResponse>>;