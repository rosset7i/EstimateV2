using Estimate.Application.Products.Services;
using Estimate.Domain.Common;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Products.Services;

public class ProductStoreTest : IUnitTestBase<ProductStore, ProductStoreMock>
{
    [Fact]
    public async Task CriarNovoProduto_QuandoCriarTemSucesso_DeveNaoRetornarErro()
    {
        //Arrange
        var productRequest = ProductUtils.CreateProductRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        await service.CreateProductAsync(productRequest);

        //Assert
        mocks.ShouldCallAddProduct()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task AtualizarProduto_QuandoAchaProduto_DeveNaoRetornarErro()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var productRequest = ProductUtils.UpdateProductRequest();
        var product = ProductUtils.Product();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchByIdAsync(productId))
            .ReturnsAsync(product);

        //Act
        await service.UpdateProductAsync(productId, productRequest);

        //Assert
        mocks.ShouldCallFetchProductById(productId)
            .ShouldCallUpdateProduct(product)
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task AtualizarProduto_QuandoNaoAchaProduto_DeveRetornarErro()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var productRequest = ProductUtils.UpdateProductRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateProductAsync(
            productId,
            productRequest));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchProductById(productId)
            .ShouldNotCallUpdateProduct()
            .ShouldNoCallUnitOfWork();
    }

    [Fact]
    public async Task RemoverProduto_QuandoAchaProduto_DeveNaoRetornarErro()
    {
        //Arrange
        var productId = Guid.NewGuid();
        var product = ProductUtils.Product();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchByIdAsync(productId))
            .ReturnsAsync(product);

        //Act
        await service.DeleteProductAsync(productId);

        //Assert
        mocks.ShouldCallFetchProductById(productId)
            .ShouldCallDeleteProduct(product)
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task RemoverProduto_QuandoNaoAchaProduto_DeveRetornarErro()
    {
        //Arrange
        var productId = Guid.NewGuid();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.DeleteProductAsync(productId));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchProductById(productId)
            .ShouldNotCallDeleteProduct()
            .ShouldNoCallUnitOfWork();
    }
    
    public ProductStoreMock GetMocks()
    {
        return new ProductStoreMock(
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public ProductStore GetClass(ProductStoreMock mocks)
    {
        return new ProductStore(
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class ProductStoreMock
{
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public ProductStoreMock(
        Mock<IProductRepository> productRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        ProductRepository = productRepository;
        UnitOfWork = unitOfWork;
    }

    public ProductStoreMock ShouldCallFetchProductById(Guid productId)
    {
        ProductRepository
            .Verify(e => e.FetchByIdAsync(productId),
                Times.Once);

        return this;
    }

    public ProductStoreMock ShouldCallAddProduct()
    {
        ProductRepository
            .Verify(e => e.AddAsync(It.IsAny<Product>()),
                Times.Once);

        return this;
    }

    public ProductStoreMock ShouldCallUpdateProduct(Product product)
    {
        ProductRepository
            .Verify(e => e.Update(product),
                Times.Once);

        return this;
    }

    public ProductStoreMock ShouldCallDeleteProduct(Product product)
    {
        ProductRepository
            .Verify(e => e.Delete(product),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }

    public ProductStoreMock ShouldNotCallUpdateProduct()
    {
        ProductRepository
            .Verify(e => e.Update(It.IsAny<Product>()),
                Times.Never);

        return this;
    }

    public ProductStoreMock ShouldNotCallDeleteProduct()
    {
        ProductRepository
            .Verify(e => e.Delete(It.IsAny<Product>()),
                Times.Never);

        return this;
    }

    public void ShouldNoCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Never);
    }
}