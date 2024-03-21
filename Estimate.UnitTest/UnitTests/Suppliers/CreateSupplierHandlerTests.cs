using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Application.Suppliers.CreateSupplierUseCase;
using Estimate.Domain.Entities;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Rossetti.Common.Result;
using Xunit;

namespace Estimate.UnitTest.UnitTests.Suppliers;

public class CreateSupplierHandlerTests : IUnitTestBase<CreateSupplierHandler, CreateSupplierHandlerMocks>
{
    [Fact]
    public async Task CreateSupplier_WhenIsSuccessful_ShouldNotReturnError()
    {
        //Arrange
        var command = SupplierUtils.CreateSupplierRequest();

        var mocks = GetMocks();
        var handler = GetClass(mocks);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Equivalent(Operation.Created, result.Result);
        mocks.ShouldCallSupplierRepositoryAdd()
            .ShouldCallUnitOfWork();
    }

    public CreateSupplierHandlerMocks GetMocks()
    {
        return new CreateSupplierHandlerMocks(
            new Mock<ISupplierRepository>(),
            new Mock<IUnitOfWork>());
    }

    public CreateSupplierHandler GetClass(CreateSupplierHandlerMocks mocks)
    {
        return new CreateSupplierHandler(
            mocks.SupplierRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class CreateSupplierHandlerMocks
{
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public CreateSupplierHandlerMocks(
        Mock<ISupplierRepository> supplierRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        SupplierRepository = supplierRepository;
        UnitOfWork = unitOfWork;
    }

    public CreateSupplierHandlerMocks ShouldCallSupplierRepositoryAdd()
    {
        SupplierRepository
            .Verify(e => e.AddAsync(It.IsAny<Supplier>()),
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