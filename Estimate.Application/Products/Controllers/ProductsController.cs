using Estimate.Application.ErrorHandling;
using Estimate.Core.Products.Dtos;
using Estimate.Core.Products.Services.Interfaces;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Infra.Repositories.Base.Models.PagingAndSorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estimate.Application.Products.Controllers;

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