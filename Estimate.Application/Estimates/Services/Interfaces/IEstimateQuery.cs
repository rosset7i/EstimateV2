using Estimate.Application.Estimates.Dtos;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;

namespace Estimate.Application.Estimates.Services.Interfaces;

public interface IEstimateQuery
{
    Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync(PagedAndSortedEstimateRequest request);
    Task<EstimateDetailsResponse> FetchEstimateDetailsByIdAsync(Guid estimateId);
}