using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Estimates.UpdateEstimateUseCase;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities.Estimate;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Estimates.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Estimates;

public class UpdateEstimateHandlerTests : IUnitTestBase<UpdateEstimateHandler, UpdateEstimateHandlerMocks>
{
    [Fact]
    public async Task UpdateEstimate_WhenEstimateIsNotNull_ShouldNotReturnError()
    {
        //Arrange
        var command = EstimateUtils.UpdateEstimateInfoRequest();
        var estimate = EstimateUtils.Estimate();
        var supplier = SupplierUtils.Supplier();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchByIdAsync(command.EstimateId))
            .ReturnsAsync(estimate);
        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(command.SupplierId))
            .ReturnsAsync(supplier);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Updated, result.Result);
        mocks.ShouldCallFetchEstimateById(command.EstimateId)
            .ShouldCallUpdateEstimate()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateEstimate_WhenEstimateIsNull_ShouldReturnNotFound()
    {
        //Arrange
        var command = EstimateUtils.UpdateEstimateInfoRequest();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<EstimateEn>(), result.FirstError);
        mocks.ShouldCallFetchEstimateById(command.EstimateId)
            .ShouldNotCallUpdateEstimate()
            .ShouldNotCallUnitOfWork();
    }

    public UpdateEstimateHandlerMocks GetMocks()
    {
        return new UpdateEstimateHandlerMocks(
            new Mock<IEstimateRepository>(),
            new Mock<ISupplierRepository>(),
            new Mock<IUnitOfWork>());
    }

    public UpdateEstimateHandler GetClass(UpdateEstimateHandlerMocks mocks)
    {
        return new UpdateEstimateHandler(
            mocks.EstimateRepository.Object,
            mocks.SupplierRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class UpdateEstimateHandlerMocks
{
    public Mock<IEstimateRepository> EstimateRepository { get; set; }
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public UpdateEstimateHandlerMocks(
        Mock<IEstimateRepository> estimateRepository,
        Mock<ISupplierRepository> supplierRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        EstimateRepository = estimateRepository;
        SupplierRepository = supplierRepository;
        UnitOfWork = unitOfWork;
    }

    public UpdateEstimateHandlerMocks ShouldCallFetchEstimateById(Guid estimateId)
    {
        EstimateRepository
            .Verify(e => e.FetchByIdAsync(estimateId),
                Times.Once);

        return this;
    }

    public UpdateEstimateHandlerMocks ShouldCallUpdateEstimate()
    {
        EstimateRepository
            .Verify(e => e.Update(It.IsAny<EstimateEn>()),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }

    public UpdateEstimateHandlerMocks ShouldNotCallUpdateEstimate()
    {
        EstimateRepository
            .Verify(e => e.Update(It.IsAny<EstimateEn>()),
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