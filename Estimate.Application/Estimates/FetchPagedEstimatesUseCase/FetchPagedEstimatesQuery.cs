using MediatR;
using Rossetti.Common.Data.Pagination;

namespace Estimate.Application.Estimates.FetchPagedEstimatesUseCase;

public record FetchPagedEstimatesQuery(
    string Name,
    Guid? SupplierId) : PagedAndSortedRequest, IRequest<PagedResultOf<EstimateResponse>>;