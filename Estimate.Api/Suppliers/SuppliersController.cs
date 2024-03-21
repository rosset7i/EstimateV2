using Estimate.Api.ErrorHandling;
using Estimate.Application.Common.Repositories;
using Estimate.Application.Suppliers.CreateSupplierUseCase;
using Estimate.Application.Suppliers.FetchPagedSuppliersUseCase;
using Estimate.Application.Suppliers.RemoveSupplierUseCase;
using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rossetti.Common.Data.Pagination;
using Rossetti.Common.Result;

namespace Estimate.Api.Suppliers;

[Route("api/v1/Suppliers")]
[Authorize]
public class SuppliersController : ApiController
{
    private readonly IMediator _mediator;

    public SuppliersController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<PagedResultOf<SupplierResponse>> FetchPagedSuppliersAsync([FromQuery] PagedAndSortedSupplierQuery query) =>
        await _mediator.Send(query);

    [HttpGet("{supplierId:guid}")]
    public async Task<Supplier?> FetchSupplierByIdAsync(
        [FromRoute] Guid supplierId,
        [FromServices] ISupplierRepository supplierRepository) =>
        await supplierRepository.FetchByIdAsync(supplierId);

    [HttpPost]
    public async Task<ResultOf<Operation>> CreateSupplierAsync([FromBody] CreateSupplierCommand command) =>
        await _mediator.Send(command);

    [HttpPut("Update")]
    public async Task<ResultOf<Operation>> UpdateSupplierAsync([FromBody] UpdateSupplierCommand command) =>
        await _mediator.Send(command);

    [HttpDelete("Delete")]
    public async Task<ResultOf<Operation>> DeleteSupplierByIdAsync([FromQuery] RemoveSupplierCommand command) =>
        await _mediator.Send(command);
}