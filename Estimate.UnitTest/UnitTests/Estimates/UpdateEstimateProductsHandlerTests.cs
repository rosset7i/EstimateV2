using Estimate.Application.Common.Helpers;
using Estimate.Application.Common.Models;
using Estimate.Application.Common.Repositories;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Estimates.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Moq;
using Rossetti.Common.Data.Repository;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Estimates;

public class UpdateEstimateProductsHandlerTests : IUnitTestBase<UpdateEstimateProductsHandler, UpdateEstimateProductsHandlerMocks>
{
    [Fact]
    public async Task UpdateEstimateProducts_WhenDoesntFindEstimate_ShouldReturnNotFound()
    {
        //Arrange
        var command = new UpdateEstimateProductsCommand(
            Guid.NewGuid(),
            EstimateUtils.UpdateEstimateProductsRequest());

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(new List<Product>());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(CommonError.NotFound<EstimateEn>(), result.FirstError);
        mocks.ShouldCallFetchEstimateWithProducts(command.EstimateId)
            .ShouldCallFetchProductsByIdsAsync(command.UpdateEstimateProductsRequest)
            .ShouldNotCallUpdateEstimate()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimateProducts_WhenDoesntProducts_ShouldReturnNotFound()
    {
        //Arrange
        var command = new UpdateEstimateProductsCommand(
            Guid.NewGuid(),
            EstimateUtils.UpdateEstimateProductsRequest());
        var estimate = EstimateUtils.Estimate();
        var productIds = CreateProductEstimateHelper.ExtractProductIds(command.UpdateEstimateProductsRequest);

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchEstimateWithProducts(command.EstimateId))
            .ReturnsAsync(estimate);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(productIds))
            .ReturnsAsync(new List<Product>());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(CommonError.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchEstimateWithProducts(command.EstimateId)
            .ShouldCallFetchProductsByIdsAsync(command.UpdateEstimateProductsRequest)
            .ShouldNotCallUpdateEstimate()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimateProducts_WhenIsSuccessful_ShouldNotReturnErrors()
    {
        //Arrange
        var command = new UpdateEstimateProductsCommand(
            Guid.NewGuid(),
            EstimateUtils.UpdateEstimateProductsRequest());
        var estimate = EstimateUtils.Estimate();
        var products = ProductUtils.Products(command.UpdateEstimateProductsRequest);
        var productIds = CreateProductEstimateHelper.ExtractProductIds(command.UpdateEstimateProductsRequest);

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchEstimateWithProducts(command.EstimateId))
            .ReturnsAsync(estimate);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(productIds))
            .ReturnsAsync(products);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Updated, result.Result);
        mocks.ShouldCallFetchEstimateWithProducts(command.EstimateId)
            .ShouldCallFetchProductsByIdsAsync(command.UpdateEstimateProductsRequest)
            .ShouldCallUpdateEstimate()
            .ShouldCallUnitOfWork();
    }

    public UpdateEstimateProductsHandlerMocks GetMocks()
    {
        return new UpdateEstimateProductsHandlerMocks(
            new Mock<IEstimateRepository>(),
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public UpdateEstimateProductsHandler GetClass(UpdateEstimateProductsHandlerMocks mocks)
    {
        return new UpdateEstimateProductsHandler(
            mocks.EstimateRepository.Object,
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class UpdateEstimateProductsHandlerMocks
{
    public Mock<IEstimateRepository> EstimateRepository { get; set; }
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public UpdateEstimateProductsHandlerMocks(
        Mock<IEstimateRepository> estimateRepository,
        Mock<IProductRepository> productRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        EstimateRepository = estimateRepository;
        ProductRepository = productRepository;
        UnitOfWork = unitOfWork;
    }

    public UpdateEstimateProductsHandlerMocks ShouldCallFetchProductsByIdsAsync(
        List<UpdateEstimateProductsRequest> request)
    {
        var productIds = CreateProductEstimateHelper.ExtractProductIds(request);

        ProductRepository
            .Verify(e => e.FetchProductsByIdsAsync(productIds),
                Times.Once);

        return this;
    }

    public UpdateEstimateProductsHandlerMocks ShouldCallFetchEstimateWithProducts(Guid estimateId)
    {
        EstimateRepository
            .Verify(e => e.FetchEstimateWithProducts(estimateId),
                Times.Once);

        return this;
    }

    public UpdateEstimateProductsHandlerMocks ShouldCallUpdateEstimate()
    {
        EstimateRepository
            .Verify(e => e.UpdateProducts(It.IsAny<EstimateEn>()),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(CancellationToken.None),
                Times.Once);
    }

    public UpdateEstimateProductsHandlerMocks ShouldNotCallUpdateEstimate()
    {
        EstimateRepository
            .Verify(e => e.Update(It.IsAny<EstimateEn>()),
                Times.Never);

        return this;
    }

    public void ShouldNotCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(CancellationToken.None),
                Times.Never);
    }
}