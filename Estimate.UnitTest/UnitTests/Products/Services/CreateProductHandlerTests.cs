using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Products.CreateProductUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Products.Services;

public class CreateProductHandlerTests : IUnitTestBase<CreateProductHandler, CreateProductHandlerMocks>
{
    [Fact]
    public async Task CreateProduct_WhenItsSucessfull_ShouldNotReturnError()
    {
        //Arrange
        var command = ProductUtils.CreateProductRequest();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        mocks.ShouldCallAddProduct()
            .ShouldCallUnitOfWork();
    }
    
    public CreateProductHandlerMocks GetMocks()
    {
        return new CreateProductHandlerMocks(
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public CreateProductHandler GetClass(CreateProductHandlerMocks mocks)
    {
        return new CreateProductHandler(
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class CreateProductHandlerMocks
{
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public CreateProductHandlerMocks(
        Mock<IProductRepository> productRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        ProductRepository = productRepository;
        UnitOfWork = unitOfWork;
    }

    public CreateProductHandlerMocks ShouldCallAddProduct()
    {
        ProductRepository
            .Verify(e => e.AddAsync(It.IsAny<Product>()),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }
}