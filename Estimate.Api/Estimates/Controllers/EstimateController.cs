using Estimate.Api.ErrorHandling;
using Estimate.Application.Common.Models.PagingAndSorting;
using Estimate.Application.Estimates.CreateEstimateUseCase;
using Estimate.Application.Estimates.FetchEstimateDetailsUseCase;
using Estimate.Application.Estimates.FetchPagedEstimatesUseCase;
using Estimate.Application.Estimates.RemoveEstimateUseCase;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Estimates.UpdateEstimateUseCase;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.Estimates.Controllers;

[Route("api/v1/Estimates")]
[Authorize]
public class EstimateController : ApiController
{
    private readonly IMediator _mediator;

    public EstimateController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync([FromQuery]FetchPagedEstimatesQuery query) =>
        await _mediator.Send(query);

    [HttpGet("FetchDetails")]
    public async Task<ResultOf<FetchEstimateDetailsResponse>> FetchEstimateDetailsByIdAsync([FromQuery]FetchEstimateDetailsQuery query) =>
        await _mediator.Send(query);

    [HttpPost]
    public async Task<ResultOf<Operation>> CreateEstimateAsync([FromBody]CreateEstimateCommand command) =>
        await _mediator.Send(command);

    [HttpPut("Update")]
    public async Task<ResultOf<Operation>> UpdateEstimateInfoAsync([FromBody]UpdateEstimateCommand command) =>
        await _mediator.Send(command);

    [HttpPut("UpdateProducts")]
    public async Task<ResultOf<Operation>> UpdateEstimateProductsAsync([FromBody]UpdateEstimateProductsCommand command) =>
        await _mediator.Send(command);

    [HttpDelete("Delete")]
    public async Task<ResultOf<Operation>> DeleteEstimateByIdAsync([FromQuery]RemoveEstimateCommand command) =>
        await _mediator.Send(command);
}