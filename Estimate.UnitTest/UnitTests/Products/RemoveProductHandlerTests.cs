using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Products.RemoveProductUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Moq;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Products;

public class RemoveProductHandlerTests : IUnitTestBase<RemoveProductHandler, RemoveProductHandlerMocks>
{
    [Fact]
    public async Task RemoveProduct_WhenProductIsFound_ShouldNotReturnError()
    {
        //Arrange
        var command = new RemoveProductCommand(Guid.NewGuid());
        var product = ProductUtils.Product();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Deleted, result.Result);
        mocks.ShouldCallFetchProductById(command.ProductId)
            .ShouldCallDeleteProduct(product)
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task RemoveProduct_WhenProductIsNotFound_ShouldReturnError()
    {
        //Arrange
        var command = new RemoveProductCommand(Guid.NewGuid());

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(CommonError.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchProductById(command.ProductId)
            .ShouldNotCallDeleteProduct()
            .ShouldNoCallUnitOfWork();
    }

    public RemoveProductHandlerMocks GetMocks()
    {
        return new RemoveProductHandlerMocks(
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public RemoveProductHandler GetClass(RemoveProductHandlerMocks mocks)
    {
        return new RemoveProductHandler(
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class RemoveProductHandlerMocks
{
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public RemoveProductHandlerMocks(
        Mock<IProductRepository> productRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        ProductRepository = productRepository;
        UnitOfWork = unitOfWork;
    }

    public RemoveProductHandlerMocks ShouldCallFetchProductById(Guid productId)
    {
        ProductRepository
            .Verify(e => e.FetchByIdAsync(productId),
                Times.Once);

        return this;
    }

    public RemoveProductHandlerMocks ShouldCallDeleteProduct(Product product)
    {
        ProductRepository
            .Verify(e => e.Delete(product),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(CancellationToken.None),
                Times.Once);
    }

    public RemoveProductHandlerMocks ShouldNotCallDeleteProduct()
    {
        ProductRepository
            .Verify(e => e.Delete(It.IsAny<Product>()),
                Times.Never);

        return this;
    }

    public void ShouldNoCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(CancellationToken.None),
                Times.Never);
    }
}