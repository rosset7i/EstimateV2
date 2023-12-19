using Estimate.Api.ErrorHandling;
using Estimate.Application.Infrastructure.Models.PagingAndSorting;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Application.Products.FetchPagedProductsUseCase;
using Estimate.Application.Products.Services.Interfaces;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.Products.Controllers;

[Route("api/v1/products")]
[Authorize]
public class ProductsController : ApiController
{
    private readonly IProductStore _productStore;
    private readonly IProductQuery _productQuery;

    public ProductsController(
        IProductStore productStore,
        IProductQuery productQuery)
    {
        _productStore = productStore;
        _productQuery = productQuery;
    }

    [HttpGet]
    public async Task<PagedResultOf<ProductResponse>> FetchPagedProductsAsync([FromQuery]PagedAndSortedProductRequest request) =>
        await _productQuery.FetchPagedProductsAsync(request);
    
    [HttpGet("{productId:guid}")]
    public async Task<Product?> FetchProductByIdAsync(
        [FromRoute]Guid productId,
        [FromServices]IProductRepository productRepository) =>
        await productRepository.FetchByIdAsync(productId);
    
    [HttpPost]
    public async Task CreateProductAsync([FromBody]CreateProductRequest request) =>
        await _productStore.CreateProductAsync(request);

    [HttpPut("{productId:guid}/update")]
    public async Task UpdateProductAsync(
        [FromRoute]Guid productId,
        [FromBody]UpdateProductRequest request) =>
        await _productStore.UpdateProductAsync(productId, request);

    [HttpDelete("{productId:guid}/delete")]
    public async Task DeleteProductAsync([FromRoute]Guid productId) =>
        await _productStore.DeleteProductAsync(productId);
}