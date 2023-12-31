﻿using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using MediatR;

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
            return DomainError.Common.NotFound<Supplier>();

        var updatedSupplier = command.UpdateInfoOf(supplier!);

        _supplierRepository.Update(updatedSupplier);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Updated;
    }
}