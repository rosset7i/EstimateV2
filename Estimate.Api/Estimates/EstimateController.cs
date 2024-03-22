using Estimate.Application.Estimates.CompareEstimatesUseCase;
using Estimate.Application.Estimates.CreateEstimateUseCase;
using Estimate.Application.Estimates.FetchEstimateDetailsUseCase;
using Estimate.Application.Estimates.FetchPagedEstimatesUseCase;
using Estimate.Application.Estimates.RemoveEstimateUseCase;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Application.Estimates.UpdateEstimateUseCase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rossetti.Common.Controller;
using Rossetti.Common.Data.Pagination;
using Rossetti.Common.Result;

namespace Estimate.Api.Estimates;

[Route("api/v1/Estimates")]
[Authorize]
public class EstimateController : ApiController
{
    private readonly IMediator _mediator;

    public EstimateController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<PagedResultOf<EstimateResponse>> FetchPagedEstimatesAsync(
        [FromQuery] FetchPagedEstimatesQuery query,
        CancellationToken cancellationToken) =>
        await _mediator.Send(query, cancellationToken);

    [HttpGet("Compare")]
    public async Task<ResultOf<List<CompareEstimatesResponse>>> CompareEstimatesAsync(
        [FromQuery] CompareEstimatesQuery query,
        CancellationToken cancellationToken) =>
        await _mediator.Send(query, cancellationToken);

    [HttpGet("FetchDetails")]
    public async Task<ResultOf<FetchEstimateDetailsResponse>> FetchEstimateDetailsByIdAsync(
        [FromQuery] FetchEstimateDetailsQuery query,
        CancellationToken cancellationToken) =>
        await _mediator.Send(query, cancellationToken);

    [HttpPost]
    public async Task<ResultOf<Operation>> CreateEstimateAsync(
        [FromBody] CreateEstimateCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpPut("Update")]
    public async Task<ResultOf<Operation>> UpdateEstimateInfoAsync(
        [FromBody] UpdateEstimateCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpPut("UpdateProducts")]
    public async Task<ResultOf<Operation>> UpdateEstimateProductsAsync(
        [FromBody] UpdateEstimateProductsCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpDelete("Delete")]
    public async Task<ResultOf<Operation>> DeleteEstimateByIdAsync(
        [FromQuery] RemoveEstimateCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);
}