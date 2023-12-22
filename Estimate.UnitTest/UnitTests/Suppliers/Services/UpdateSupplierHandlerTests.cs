using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Suppliers.Services;

public class UpdateSupplierHandlerTests : IUnitTestBase<UpdateSupplierHandler, UpdateSupplierHandleMocks>
{
    [Fact]
    public async Task UpdateSupplier_WhenSupplierIsFound_ShouldNotReturnError()
    {
        //Arrange
        var command = SupplierUtils.UpdateSupplierRequest();
        var supplier = SupplierUtils.Supplier();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(command.SupplierId))
            .ReturnsAsync(supplier);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        mocks.ShouldCallSupplierRepositoryFetchById(command.SupplierId)
            .ShouldCallSupplierRepositoryUpdate(supplier)
            .ShouldCallUnitOfWork();
    }

    [Fact]
    public async Task UpdateSupplier_WhenSupplierIsNotFound_ShouldReturnError()
    {
        //Arrange
        var command = SupplierUtils.UpdateSupplierRequest();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Supplier>(), result.FirstError);
        mocks.ShouldCallSupplierRepositoryFetchById(command.SupplierId)
            .ShouldNotCallSupplierRepositoryUpdate()
            .ShouldNotCallUnitOfWork();
    }
    
    public UpdateSupplierHandleMocks GetMocks()
    {
        return new UpdateSupplierHandleMocks(
            new Mock<ISupplierRepository>(),
            new Mock<IUnitOfWork>());
    }

    public UpdateSupplierHandler GetClass(UpdateSupplierHandleMocks mocks)
    {
        return new UpdateSupplierHandler(
            mocks.SupplierRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class UpdateSupplierHandleMocks
{
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public UpdateSupplierHandleMocks(
        Mock<ISupplierRepository> supplierRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        SupplierRepository = supplierRepository;
        UnitOfWork = unitOfWork;
    }

    public UpdateSupplierHandleMocks ShouldCallSupplierRepositoryFetchById(Guid fornecedorId)
    {
        SupplierRepository
            .Verify(e => e.FetchByIdAsync(fornecedorId),
                Times.Once);

        return this;
    }

    public UpdateSupplierHandleMocks ShouldCallSupplierRepositoryUpdate(Supplier supplier)
    {
        SupplierRepository
            .Verify(e => e.Update(supplier),
                Times.Once);

        return this;
    }

    public void ShouldCallUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);
    }

    public UpdateSupplierHandleMocks ShouldNotCallSupplierRepositoryUpdate()
    {
        SupplierRepository
            .Verify(e => e.Update(It.IsAny<Supplier>()),
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