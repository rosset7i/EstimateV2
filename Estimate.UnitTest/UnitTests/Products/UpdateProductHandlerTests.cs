using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Products.UpdateProductUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Moq;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Products;

public class UpdateProductHandlerTests : IUnitTestBase<UpdateProductHandler, UpdateProductHandlerMocks>
{
    [Fact]
    public async Task UpdateProduct_WhenProductIsFound_ShouldNotReturnError()
    {
        //Arrange
        var command = ProductUtils.UpdateProductRequest();
        var product = ProductUtils.Product();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Updated, result.Result);
        mocks.ShouldCallFetchProductById(command.ProductId)
            .ShouldCallUpdateProduct(product)
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateProduct_WhenProductIsNotFound_ShouldReturnError()
    {
        //Arrange
        var command = ProductUtils.UpdateProductRequest();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(CommonError.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchProductById(command.ProductId)
            .ShouldNotCallUpdateProduct()
            .ShouldNoCallUnitOfWork();
    }

    public UpdateProductHandlerMocks GetMocks()
    {
        return new UpdateProductHandlerMocks(
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public UpdateProductHandler GetClass(UpdateProductHandlerMocks mocks)
    {
        return new UpdateProductHandler(
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class UpdateProductHandlerMocks
{
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public UpdateProductHandlerMocks(
        Mock<IProductRepository> productRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        ProductRepository = productRepository;
        UnitOfWork = unitOfWork;
    }

    public UpdateProductHandlerMocks ShouldCallFetchProductById(Guid productId)
    {
        ProductRepository
            .Verify(e => e.FetchByIdAsync(productId),
                Times.Once);

        return this;
    }

    public UpdateProductHandlerMocks ShouldCallUpdateProduct(Product product)
    {
        ProductRepository
            .Verify(e => e.Update(product),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(CancellationToken.None),
                Times.Once);
    }

    public UpdateProductHandlerMocks ShouldNotCallUpdateProduct()
    {
        ProductRepository
            .Verify(e => e.Update(It.IsAny<Product>()),
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