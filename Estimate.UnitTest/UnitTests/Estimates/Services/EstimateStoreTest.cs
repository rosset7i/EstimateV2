using Estimate.Application.Estimates.UpdateEstimateProductsUseCase;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Estimates.TestUtils;
using Estimate.UnitTest.UnitTests.Products.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Xunit;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.UnitTest.UnitTests.Estimates.Services;

public class EstimateStoreTest : IUnitTestBase<EstimateStore, EstimateStoreMock>
{
    [Fact]
    public async Task CreateEstimate_WhenSuccessful_ShouldNotReturnError()
    {
        //Arrange
        var estimateRequest = EstimateUtils.CreateEstimateRequest();
        var supplier = SupplierUtils.Supplier();
        var products = ProductUtils.Products(estimateRequest.ProductsInEstimate);

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(estimateRequest.SupplierId))
            .ReturnsAsync(supplier);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(products);

        //Act
        await service.CreateEstimateAsync(estimateRequest);

        //Assert
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
        var service = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(productIds))
            .ReturnsAsync(products);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service
            .CreateEstimateAsync(estimateRequest));

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
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.CreateEstimateAsync(estimateRequest));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchSupplierById(estimateRequest.SupplierId)
            .ShouldCallFetchProductsByIdsAsync(estimateRequest.ProductsInEstimate)
            .ShouldNotCallFetchEstimateWithProducts()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimateProducts_WhenDoesntFindEstimate_ShouldReturnNotFound()
    {
        //Arrange
        var estimateId = Guid.NewGuid();
        var request = EstimateUtils.UpdateEstimateProductsRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(new List<Product>());

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateEstimateProductsAsync(
            estimateId,
            request));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<EstimateEn>(), result.FirstError);
        mocks.ShouldCallFetchEstimateWithProducts(estimateId)
            .ShouldCallFetchProductsByIdsAsync(request)
            .ShouldNotCallUpdateEstimate()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimateProducts_WhenDoesntProducts_ShouldReturnNotFound()
    {
        //Arrange
        var estimateId = Guid.NewGuid();
        var request = EstimateUtils.UpdateEstimateProductsRequest();
        var estimate = EstimateUtils.Estimate();
        var productIds = UpdateEstimateProductsRequest
            .ExtractProductIds(request);

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchEstimateWithProducts(estimateId))
            .ReturnsAsync(estimate);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(productIds))
            .ReturnsAsync(new List<Product>());

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateEstimateProductsAsync(
            estimateId,
            request));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Product>(), result.FirstError);
        mocks.ShouldCallFetchEstimateWithProducts(estimateId)
            .ShouldCallFetchProductsByIdsAsync(request)
            .ShouldNotCallUpdateEstimate()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimateProducts_WhenIsSuccessful_ShouldNotReturnErrors()
    {
        //Arrange
        var estimateId = Guid.NewGuid();
        var estimate = EstimateUtils.Estimate();
        var request = EstimateUtils.UpdateEstimateProductsRequest();
        var products = ProductUtils.Products(request);
        var productIds = UpdateEstimateProductsRequest
            .ExtractProductIds(request);

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchEstimateWithProducts(estimateId))
            .ReturnsAsync(estimate);
        mocks.ProductRepository
            .Setup(e => e.FetchProductsByIdsAsync(productIds))
            .ReturnsAsync(products);

        //Act
        await service.UpdateEstimateProductsAsync(
            estimateId,
            request);

        //Assert
        mocks.ShouldCallFetchEstimateWithProducts(estimateId)
            .ShouldCallFetchProductsByIdsAsync(request)
            .ShouldCallUpdateEstimate()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimate_WhenEstimateIsNotNull_ShouldNotReturnError()
    {
        //Arrange
        var estimateId = Guid.NewGuid();
        var estimate = EstimateUtils.Estimate();
        var supplier = SupplierUtils.Supplier();
        var estimateInfoRequest = EstimateUtils.UpdateEstimateInfoRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchByIdAsync(estimateId))
            .ReturnsAsync(estimate);
        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(estimateInfoRequest.SupplierId))
            .ReturnsAsync(supplier);

        //Act
        await service.UpdateEstimateInfoAsync(
            estimateId,
            estimateInfoRequest);

        //Assert
        mocks.ShouldCallFetchEstimateById(estimateId)
            .ShouldCallUpdateEstimate()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimate_WhenEstimateIsNull_ShouldReturnNotFound()
    {
        //Arrange
        var estimateId = Guid.NewGuid();
        var estimateInfoRequest = EstimateUtils.UpdateEstimateInfoRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateEstimateInfoAsync(
            estimateId,
            estimateInfoRequest));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<EstimateEn>(), result.FirstError);
        mocks.ShouldCallFetchEstimateById(estimateId)
            .ShouldNotCallUpdateEstimate()
            .ShouldNotCallUnitOfWork();
    }

    [Fact]
    public async Task DeleteEstimate_WhenEstimateIsNotNull_ShouldNotReturnError()
    {
        //Arrange
        var estimateId = Guid.NewGuid();
        var estimate = EstimateUtils.Estimate();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchByIdAsync(estimateId))
            .ReturnsAsync(estimate);

        //Act
        await service.DeleteEstimateByIdAsync(estimateId);

        //Assert
        mocks.ShouldCallFetchEstimateById(estimateId)
            .ShouldCallDeleteEstimate()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task DeleteEstimate_WhenEstimateIsNull_ShouldReturnError()
    {
        //Arrange
        var estimateId = Guid.NewGuid();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.DeleteEstimateByIdAsync(estimateId));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<EstimateEn>(), result.FirstError);
        mocks.ShouldCallFetchEstimateById(estimateId)
            .ShouldNotCallDeleteEstimate()
            .ShouldNotCallUnitOfWork();
    }
    
    public EstimateStoreMock GetMocks()
    {
        return new EstimateStoreMock(
            new Mock<IEstimateRepository>(),
            new Mock<ISupplierRepository>(),
            new Mock<IProductRepository>(),
            new Mock<IUnitOfWork>());
    }

    public EstimateStore GetClass(EstimateStoreMock mocks)
    {
        return new EstimateStore(
            mocks.EstimateRepository.Object,
            mocks.SupplierRepository.Object,
            mocks.ProductRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class EstimateStoreMock
{
    public Mock<IEstimateRepository> EstimateRepository { get; set; }
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IProductRepository> ProductRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public EstimateStoreMock(
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

    public EstimateStoreMock ShouldCallFetchSupplierById(Guid supplierId)
    {
        SupplierRepository
            .Verify(e => e.FetchByIdAsync(supplierId),
                Times.Once);

        return this;
    }
    
    public EstimateStoreMock ShouldCallFetchProductsByIdsAsync(
        List<UpdateEstimateProductsRequest> request)
    {
        var productIds = UpdateEstimateProductsRequest
            .ExtractProductIds(request);

        ProductRepository
            .Verify(e => e.FetchProductsByIdsAsync(productIds),
                Times.Once);

        return this;
    }
    
    public EstimateStoreMock ShouldCallAddEstimate()
    {
        EstimateRepository
            .Verify(e => e.AddAsync(It.IsAny<EstimateEn>()),
                Times.Once);

        return this;
    }

    public EstimateStoreMock ShouldCallFetchEstimateWithProducts(Guid estimateId)
    {
        EstimateRepository
            .Verify(e => e.FetchEstimateWithProducts(estimateId),
                Times.Once);

        return this;
    }
    
    public EstimateStoreMock ShouldCallFetchEstimateById(Guid estimateId)
    {
        EstimateRepository
            .Verify(e => e.FetchByIdAsync(estimateId),
                Times.Once);

        return this;
    }
    
    public EstimateStoreMock ShouldCallUpdateEstimate()
    {
        EstimateRepository
            .Verify(e => e.Update(It.IsAny<EstimateEn>()),
                Times.Once);

        return this;
    }
    
    public EstimateStoreMock ShouldCallDeleteEstimate()
    {
        EstimateRepository
            .Verify(e => e.Delete(It.IsAny<EstimateEn>()),
                Times.Once);

        return this;
    }
    
    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }
    
    public EstimateStoreMock ShouldNotCallFetchEstimateWithProducts()
    {
        EstimateRepository
            .Verify(e => e.FetchEstimateWithProducts(It.IsAny<Guid>()),
                Times.Never);

        return this;
    }
    
    public EstimateStoreMock ShouldNotCallUpdateEstimate()
    {
        EstimateRepository
            .Verify(e => e.Update(It.IsAny<EstimateEn>()),
                Times.Never);

        return this;
    }
    
    public EstimateStoreMock ShouldNotCallDeleteEstimate()
    {
        EstimateRepository
            .Verify(e => e.Delete(It.IsAny<EstimateEn>()),
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