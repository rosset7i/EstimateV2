using Estimate.Api.ErrorHandling;
using Estimate.Application.Common.Repositories;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.FetchPagedProductsUseCase;
using Estimate.Application.Products.RemoveProductUseCase;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rossetti.Common.Data.Pagination;
using Rossetti.Common.Result;

namespace Estimate.Api.Products;

[Route("api/v1/Products")]
[Authorize]
public class ProductsController : ApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync([FromQuery] PagedAndSortedProductQuery query,
        CancellationToken cancellationToken) =>
        await _mediator.Send(query, cancellationToken);

    [HttpGet("{productId:guid}")]
    public async Task<Product?> FetchProductByIdAsync(
        [FromRoute] Guid productId,
        [FromServices] IProductRepository productRepository) =>
        await productRepository.FetchByIdAsync(productId);

    [HttpPost]
    public async Task<ResultOf<Operation>> CreateProductAsync([FromBody] CreateProductCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpPut("Update")]
    public async Task<ResultOf<Operation>> UpdateProductAsync([FromBody] UpdateProductCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpDelete("Delete")]
    public async Task<ResultOf<Operation>> DeleteProductAsync([FromQuery] RemoveProductCommand command,
        CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);
}