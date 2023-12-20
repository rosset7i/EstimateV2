using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;

namespace Estimate.Application.Suppliers.CreateSupplierUseCase;

public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, CreateSupplierResult>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSupplierHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateSupplierResult> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        var newSupplier = new Supplier(
            Guid.NewGuid(),
            command.Name);

        await _supplierRepository.AddAsync(newSupplier);
        await _unitOfWork.SaveChangesAsync();

        return new CreateSupplierResult();
    }
}