using Estimate.Core.Estimates.Dtos;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Core.Estimates.Services.Interfaces;

public interface IEstimateQuery
{
    Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync(PagedAndSortedEstimateRequest request);
    Task<EstimateDetailsResponse> FetchEstimateDetailsByIdAsync(Guid estimateId);
}