using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Estimates.CreateEstimateUseCase;
using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Estimates.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Estimates;

public class CreateEstimateHandlerTests : IUnitTestBase<CreateEstimateHandler, CreateEstimateHandlerMocks>
{
    [Fact]
    public async Task CreateEstimate_WhenSuccessful_ShouldNotReturnError()
    {
        //Arrange
        var estimateRequest = EstimateUtils.CreateEstimateRequest();
        var supplier = SupplierUtils.Supplier();
        var products = ProductUtils.Products(estimateRequest.ProductsInEstimate);

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(estimateRequest.SupplierId))
            .ReturnsAsync(supplier);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(products);

        //Act
        var result = await handler.Handle(estimateRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Created, result.Result);
        mocks.ShouldCallFetchSupplierById(estimateRequest.SupplierId)
            .ShouldCallFetchProductsByIdsAsync(estimateRequest.ProductsInEstimate)
            .ShouldCallAddEstimate()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task CreateEstimate_WhenSupplierIsNull_ShouldReturnNotFound()
    {
        //Arrange
        var estimateRequest = EstimateUtils.CreateEstimateRequest();
        var products = ProductUtils.Products(estimateRequest.ProductsInEstimate);
        var productIds = UpdateEstimateProductsRequest
            .ExtractProductIds(estimateRequest.ProductsInEstimate);

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(productIds))
            .ReturnsAsync(products);

        //Act
        var result = await handler.Handle(estimateRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Supplier>(), result.FirstError);
        mocks.ShouldCallFetchSupplierById(estimateRequest.SupplierId)
            .ShouldCallFetchProductsByIdsAsync(estimateRequest.ProductsInEstimate)
            .ShouldNotCallFetchEstimateWithProducts()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task CreateEstimate_WhenDoesntFindProducts_ShouldReturnNotFound()
    {
        //Arrange
        var estimateRequest = EstimateUtils.CreateEstimateRequest();
        var supplier = SupplierUtils.Supplier();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(estimateRequest.SupplierId))
            .ReturnsAsync(supplier);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(new List<Product>());

        //Act
        var result = await service.Handle(estimateRequest, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchSupplierById(estimateRequest.SupplierId)
            .ShouldCallFetchProductsByIdsAsync(estimateRequest.ProductsInEstimate)
            .ShouldNotCallFetchEstimateWithProducts()
            .ShouldNotCallUnitOfWork();
    }

    public CreateEstimateHandlerMocks GetMocks()
    {
        return new CreateEstimateHandlerMocks(
            new Mock<IEstimateRepository>(),
            new Mock<ISupplierRepository>(),
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public CreateEstimateHandler GetClass(CreateEstimateHandlerMocks mocks)
    {
        return new CreateEstimateHandler(
            mocks.EstimateRepository.Object,
            mocks.SupplierRepository.Object,
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class CreateEstimateHandlerMocks
{
    public Mock<IEstimateRepository> EstimateRepository { get; set; }
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public CreateEstimateHandlerMocks(
        Mock<IEstimateRepository> estimateRepository,
        Mock<ISupplierRepository> supplierRepository,
        Mock<IProductRepository> productRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        EstimateRepository = estimateRepository;
        SupplierRepository = supplierRepository;
        ProductRepository = productRepository;
        UnitOfWork = unitOfWork;
    }

    public CreateEstimateHandlerMocks ShouldCallFetchSupplierById(Guid supplierId)
    {
        SupplierRepository
            .Verify(e => e.FetchByIdAsync(supplierId),
                Times.Once);

        return this;
    }

    public CreateEstimateHandlerMocks ShouldCallFetchProductsByIdsAsync(
        List<UpdateEstimateProductsRequest> request)
    {
        var productIds = UpdateEstimateProductsRequest
            .ExtractProductIds(request);

        ProductRepository
            .Verify(e => e.FetchProductsByIdsAsync(productIds),
                Times.Once);

        return this;
    }

    public CreateEstimateHandlerMocks ShouldCallAddEstimate()
    {
        EstimateRepository
            .Verify(e => e.AddAsync(It.IsAny<EstimateEn>()),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }

    public CreateEstimateHandlerMocks ShouldNotCallFetchEstimateWithProducts()
    {
        EstimateRepository
            .Verify(e => e.FetchEstimateWithProducts(It.IsAny<Guid>()),
                Times.Never);

        return this;
    }

    public void ShouldNotCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Never);
    }
}