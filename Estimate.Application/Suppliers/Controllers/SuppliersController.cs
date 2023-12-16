using Estimate.Application.ErrorHandling;
using Estimate.Core.Suppliers.Dtos;
using Estimate.Core.Suppliers.Services.Interfaces;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Application.Suppliers.Controllers;

[Route("api/v1/suppliers")]
[Authorize]
public class SuppliersController : ApiController
{
    private readonly ISupplierQuery _supplierQuery;
    private readonly ISupplierStore _supplierStore;

    public SuppliersController(
        ISupplierQuery supplierQuery,
        ISupplierStore supplierStore)
    {
        _supplierQuery = supplierQuery;
        _supplierStore = supplierStore;
    }

    [HttpGet]
    public async Task<PagedResultOf<SupplierResponse>> FetchPagedSuppliersAsync([FromQuery]PagedAndSortedSupplierRequest request) =>
        await _supplierQuery.FetchPagedSuppliersAsync(request);
    
    [HttpGet("{supplierId:guid}")]
    public async Task<Supplier?> FetchSupplierByIdAsync(
        [FromRoute]Guid supplierId,
        [FromServices]ISupplierRepository supplierRepository) =>
        await supplierRepository.FetchByIdAsync(supplierId);
    
    [HttpPost]
    public async Task CreateSupplierAsync([FromBody]CreateSupplierRequest request) =>
        await _supplierStore.CreateSupplierAsync(request);

    [HttpPut("{supplierId:guid}/update")]
    public async Task UpdateSupplierAsync(
        [FromRoute]Guid supplierId,
        [FromBody]UpdateSupplierInfoRequest request) =>
        await _supplierStore.UpdateSupplierAsync(supplierId, request);

    [HttpDelete("{supplierId:guid}/delete")]
    public async Task DeleteSupplierByIdAsync([FromRoute]Guid supplierId) =>
        await _supplierStore.DeleteSupplierByIdAsync(supplierId);
}