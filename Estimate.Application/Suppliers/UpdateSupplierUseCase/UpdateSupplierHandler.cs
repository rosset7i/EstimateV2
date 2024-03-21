using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities;
using MediatR;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;

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
            return CommonError.NotFound<Supplier>();

        supplier.AlterName(command.Name);

        _supplierRepository.Update(supplier);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Updated;
    }
}