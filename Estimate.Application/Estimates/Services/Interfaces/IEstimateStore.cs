using Estimate.Application.Estimates.Dtos;

namespace Estimate.Application.Estimates.Services.Interfaces;

public interface IEstimateStore
{
    Task CreateEstimateAsync(CreateEstimateRequest request);
    
    Task UpdateEstimateInfoAsync(
        Guid estimateId,
        UpdateEstimateInfoRequest request);

    Task UpdateEstimateProductsAsync(
        Guid estimateId,
        List<UpdateEstimateProductsRequest> request);

    Task DeleteEstimateByIdAsync(Guid estimateId);
}