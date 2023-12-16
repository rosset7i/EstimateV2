using Estimate.Application.ErrorHandling;
using Estimate.Core.Estimates.Dtos;
using Estimate.Core.Estimates.Services.Interfaces;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Application.Estimates.Controllers;

[Route("api/v1/estimates")]
[Authorize]
public class EstimateController : ApiController
{
    private readonly IEstimateQuery _estimateQuery;
    private readonly IEstimateStore _estimateStore;

    public EstimateController(
        IEstimateQuery estimateQuery,
        IEstimateStore estimateStore)
    {
        _estimateQuery = estimateQuery;
        _estimateStore = estimateStore;
    }

    [HttpGet]
    public async Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync([FromQuery]PagedAndSortedEstimateRequest request) =>
        await _estimateQuery.FetchPagedEstimatesAsync(request);

    [HttpGet("{estimateId:guid}")]
    public async Task<EstimateDetailsResponse> FetchEstimateDetailsByIdAsync([FromRoute]Guid estimateId) =>
        await _estimateQuery.FetchEstimateDetailsByIdAsync(estimateId);

    [HttpPost]
    public async Task CreateEstimateAsync([FromBody]CreateEstimateRequest request) =>
        await _estimateStore.CreateEstimateAsync(request);

    [HttpPut("{estimateId:guid}/update")]
    public async Task UpdateEstimateInfoAsync(
        [FromRoute]Guid estimateId,
        [FromBody]UpdateEstimateInfoRequest request) =>
        await _estimateStore.UpdateEstimateInfoAsync(estimateId, request);

    [HttpPut("{estimateId:guid}/updateProducts")]
    public async Task UpdateEstimateProductsAsync(
        [FromRoute]Guid estimateId,
        [FromBody]List<UpdateEstimateProductsRequest> request) =>
        await _estimateStore.UpdateEstimateProductsAsync(estimateId, request);

    [HttpDelete("{estimateId:guid}/delete")]
    public async Task DeleteEstimateByIdAsync([FromRoute]Guid estimateId) =>
        await _estimateStore.DeleteEstimateByIdAsync(estimateId);
}