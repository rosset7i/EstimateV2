using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Suppliers.RemoveSupplierUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Suppliers;

public class RemoveSupplierHandlerTests : IUnitTestBase<RemoveSupplierHandler, RemoveSupplierHandlerMocks>
{
    [Fact]
    public async Task RemoveSupplier_WhenSupplierIsFound_ShouldNotReturnError()
    {
        //Arrange
        var command = new RemoveSupplierCommand(Guid.NewGuid());
        var supplier = SupplierUtils.Supplier();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(command.SupplierId))
            .ReturnsAsync(supplier);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Deleted, result.Result);
        mocks.ShouldCallSupplierRepositoryFetchById(command.SupplierId)
            .ShouldCallSupplierRepositoryDelete(supplier)
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task RemoveSupplier_WhenSupplierIsNotFound_ShouldReturnError()
    {
        //Arrange
        var command = new RemoveSupplierCommand(Guid.NewGuid());

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(CommonError.NotFound<Supplier>(), result.FirstError);
        mocks.ShouldCallSupplierRepositoryFetchById(command.SupplierId)
            .ShouldNotCallSupplierRepositoryDelete()
            .ShouldNotCallUnitOfWork();
    }

    public RemoveSupplierHandlerMocks GetMocks()
    {
        return new RemoveSupplierHandlerMocks(
            new Mock<ISupplierRepository>(),
            new Mock<IUnitOfWork>());
    }

    public RemoveSupplierHandler GetClass(RemoveSupplierHandlerMocks mocks)
    {
        return new RemoveSupplierHandler(
            mocks.SupplierRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class RemoveSupplierHandlerMocks
{
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public RemoveSupplierHandlerMocks(
        Mock<ISupplierRepository> supplierRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        SupplierRepository = supplierRepository;
        UnitOfWork = unitOfWork;
    }

    public RemoveSupplierHandlerMocks ShouldCallSupplierRepositoryFetchById(Guid fornecedorId)
    {
        SupplierRepository
            .Verify(e => e.FetchByIdAsync(fornecedorId),
                Times.Once);

        return this;
    }

    public RemoveSupplierHandlerMocks ShouldCallSupplierRepositoryDelete(Supplier supplier)
    {
        SupplierRepository
            .Verify(e => e.Delete(supplier),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }

    public RemoveSupplierHandlerMocks ShouldNotCallSupplierRepositoryDelete()
    {
        SupplierRepository
            .Verify(e => e.Delete(It.IsAny<Supplier>()),
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