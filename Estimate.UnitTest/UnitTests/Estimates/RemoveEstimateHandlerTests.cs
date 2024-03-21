using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Estimates.RemoveEstimateUseCase;
using Estimate.Domain.Entities.Estimate;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Estimates.TestUtils;
using Moq;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Estimates;

public class RemoveEstimateHandlerTests : IUnitTestBase<RemoveEstimateHandler, RemoveEstimateHandlerMocks>
{
    [Fact]
    public async Task DeleteEstimate_WhenEstimateIsNotNull_ShouldNotReturnError()
    {
        //Arrange
        var command = new RemoveEstimateCommand(Guid.NewGuid());
        var estimate = EstimateUtils.Estimate();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.EstimateRepository
            .Setup(e => e.FetchByIdAsync(command.EstimateId))
            .ReturnsAsync(estimate);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Deleted, result.Result);
        mocks.ShouldCallFetchEstimateById(command.EstimateId)
            .ShouldCallDeleteEstimate()
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task DeleteEstimate_WhenEstimateIsNull_ShouldReturnError()
    {
        //Arrange
        var command = new RemoveEstimateCommand(Guid.NewGuid());

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await service.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(CommonError.NotFound<EstimateEn>(), result.FirstError);
        mocks.ShouldCallFetchEstimateById(command.EstimateId)
            .ShouldNotCallDeleteEstimate()
            .ShouldNotCallUnitOfWork();
    }

    public RemoveEstimateHandlerMocks GetMocks()
    {
        return new RemoveEstimateHandlerMocks(
            new Mock<IEstimateRepository>(),
            new Mock<IUnitOfWork>());
    }

    public RemoveEstimateHandler GetClass(RemoveEstimateHandlerMocks mocks)
    {
        return new RemoveEstimateHandler(
            mocks.EstimateRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class RemoveEstimateHandlerMocks
{
    public Mock<IEstimateRepository> EstimateRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public RemoveEstimateHandlerMocks(
        Mock<IEstimateRepository> estimateRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        EstimateRepository = estimateRepository;
        UnitOfWork = unitOfWork;
    }

    public RemoveEstimateHandlerMocks ShouldCallFetchEstimateById(Guid estimateId)
    {
        EstimateRepository
            .Verify(e => e.FetchByIdAsync(estimateId),
                Times.Once);

        return this;
    }

    public RemoveEstimateHandlerMocks ShouldCallDeleteEstimate()
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

    public RemoveEstimateHandlerMocks ShouldNotCallDeleteEstimate()
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