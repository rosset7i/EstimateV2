using Estimate.Domain.Common;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Suppliers.UpdateSupplierUseCase;

public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, ResultOf<Operation>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSupplierHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultOf<Operation>> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.FetchByIdAsync(command.SupplierId);

        if (supplier is null)
            DomainError.Common.NotFound<Supplier>();

        var updatedSupplier = command.UpdateSupplierInfoRequest.UpdateInfoOf(supplier);

        _supplierRepository.Update(updatedSupplier);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Updated;
    }
}