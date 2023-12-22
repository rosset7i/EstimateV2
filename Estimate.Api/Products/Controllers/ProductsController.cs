using Estimate.Api.ErrorHandling;
using Estimate.Application.Common.Models.PagingAndSorting;
using Estimate.Application.Common.Repositories;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.FetchPagedProductsUseCase;
using Estimate.Application.Products.RemoveProductUseCase;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.Products.Controllers;

[Route("api/v1/Products")]
[Authorize]
public class ProductsController : ApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync([FromQuery]PagedAndSortedProductQuery query) =>
        await _mediator.Send(query);
    
    [HttpGet("{productId:guid}")]
    public async Task<Product?> FetchProductByIdAsync(
        [FromRoute]Guid productId,
        [FromServices]IProductRepository productRepository) =>
        await productRepository.FetchByIdAsync(productId);
    
    [HttpPost]
    public async Task<ResultOf<Operation>> CreateProductAsync([FromBody]CreateProductCommand command) =>
        await _mediator.Send(command);

    [HttpPut("Update")]
    public async Task<ResultOf<Operation>> UpdateProductAsync([FromBody]UpdateProductCommand command) =>
        await _mediator.Send(command);

    [HttpDelete("Delete")]
    public async Task<ResultOf<Operation>> DeleteProductAsync([FromQuery]RemoveProductCommand command) =>
        await _mediator.Send(command);
}