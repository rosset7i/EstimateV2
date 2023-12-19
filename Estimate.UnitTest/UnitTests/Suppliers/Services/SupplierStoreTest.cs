using Estimate.Application.Suppliers.UpdateSupplierUseCase;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using Estimate.UnitTest.TestUtils;
using Estimate.UnitTest.UnitTests.Suppliers.TestUtils;
using Moq;
using Xunit;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.UnitTest.UnitTests.Suppliers.Services;

public class SupplierStoreTest : IUnitTestBase<UpdateSupplierHandler, SupplierStoreMock>
{
    [Fact]
    public async Task CriarNovoFornecedor_QuandoCriarTemSucesso_DeveNaoRetornarErro()
    {
        //Arrange
        var supplierRequest = SupplierUtils.CreateSupplierRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        await service.CreateSupplierAsync(supplierRequest);
        
        //Assert
        mocks.DeveChamarFornecedorRepositorioParaAdicionar()
            .DeveChamarUnitOfWork();
    }

    [Fact]
    public async Task AtualizarFornecedor_QuandoAchaFornecedor_DeveNaoRetornarErro()
    {
        //Arrange
        var supplierId = Guid.NewGuid();
        var supplierRequest = SupplierUtils.UpdateSupplierRequest();
        var supplier = SupplierUtils.Supplier();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(supplierId))
            .ReturnsAsync(supplier);

        //Act
        await service.UpdateSupplierAsync(
            supplierId,
            supplierRequest);

        //Assert
        mocks.DeveChamarFornecedorRepositorioParaBuscar(supplierId)
            .DeveChamarFornecedorRepositorioParaAtualizar(supplier)
            .DeveChamarUnitOfWork();
    }

    [Fact]
    public async Task AtualizarFornecedor_QuandoNaoAchaFornecedor_DeveRetornarErro()
    {
        //Arrange
        var supplierId = Guid.NewGuid();
        var supplierRequest = SupplierUtils.UpdateSupplierRequest();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.UpdateSupplierAsync(
            supplierId,
            supplierRequest));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Supplier>(), result.FirstError);
        mocks.DeveChamarFornecedorRepositorioParaBuscar(supplierId)
            .NaoDeveChamarFornecedorRepositorioParaAtualizar()
            .NaoDeveChamarUnitOfWork();
    }

    [Fact]
    public async Task RemoverFornecedor_QuandoAchaFornecedor_DeveNaoRetornarErro()
    {
        //Arrange
        var supplierId = Guid.NewGuid();
        var supplier = SupplierUtils.Supplier();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        mocks.SupplierRepository
            .Setup(e => e.FetchByIdAsync(supplierId))
            .ReturnsAsync(supplier);

        //Act
        await service.DeleteSupplierByIdAsync(supplierId);

        //Assert
        mocks.DeveChamarFornecedorRepositorioParaBuscar(supplierId)
            .DeveChamarFornecedorRepositorioRemover(supplier)
            .DeveChamarUnitOfWork();
    }

    [Fact]
    public async Task RemoverFornecedor_QuandoNaoAchaFornecedor_DeveRetornarErro()
    {
        //Arrange
        var supplierId = Guid.NewGuid();

        var mocks = GetMocks();
        var service = GetClass(mocks);

        //Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => service.DeleteSupplierByIdAsync(supplierId));

        //Assert
        Assert.Equivalent(DomainError.Common.NotFound<Supplier>(), result.FirstError);
        mocks.DeveChamarFornecedorRepositorioParaBuscar(supplierId)
            .NaoDeveChamarFornecedorRepositorioRemover()
            .NaoDeveChamarUnitOfWork();
    }
    
    public SupplierStoreMock GetMocks()
    {
        return new SupplierStoreMock(
            new Mock<ISupplierRepository>(),
            new Mock<IUnitOfWork>());
    }

    public UpdateSupplierHandler GetClass(SupplierStoreMock mocks)
    {
        return new UpdateSupplierHandler(
            mocks.SupplierRepository.Object,
            mocks.UnitOfWork.Object);
    }
}

public class SupplierStoreMock
{
    public Mock<ISupplierRepository> SupplierRepository { get; set; }
    public Mock<IUnitOfWork> UnitOfWork { get; set; }

    public SupplierStoreMock(
        Mock<ISupplierRepository> supplierRepository,
        Mock<IUnitOfWork> unitOfWork)
    {
        SupplierRepository = supplierRepository;
        UnitOfWork = unitOfWork;
    }

    public SupplierStoreMock DeveChamarFornecedorRepositorioParaBuscar(Guid fornecedorId)
    {
        SupplierRepository
            .Verify(e => e.FetchByIdAsync(fornecedorId),
                Times.Once);

        return this;
    }

    public SupplierStoreMock DeveChamarFornecedorRepositorioParaAdicionar()
    {
        SupplierRepository
            .Verify(e => e.AddAsync(It.IsAny<Supplier>()),
                Times.Once);

        return this;
    }

    public SupplierStoreMock DeveChamarFornecedorRepositorioParaAtualizar(Supplier supplier)
    {
        SupplierRepository
            .Verify(e => e.Update(supplier),
                Times.Once);

        return this;
    }

    public SupplierStoreMock DeveChamarFornecedorRepositorioRemover(Supplier supplier)
    {
        SupplierRepository
            .Verify(e => e.Delete(supplier),
                Times.Once);

        return this;
    }

    public SupplierStoreMock DeveChamarUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Once);

        return this;
    }

    public SupplierStoreMock NaoDeveChamarFornecedorRepositorioParaAtualizar()
    {
        SupplierRepository
            .Verify(e => e.Update(It.IsAny<Supplier>()),
                Times.Never);

        return this;
    }

    public SupplierStoreMock NaoDeveChamarFornecedorRepositorioRemover()
    {
        SupplierRepository
            .Verify(e => e.Delete(It.IsAny<Supplier>()),
                Times.Never);

        return this;
    }

    public SupplierStoreMock NaoDeveChamarUnitOfWork()
    {
        UnitOfWork
            .Verify(e => e.SaveChangesAsync(),
                Times.Never);

        return this;
    }

}