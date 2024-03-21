﻿using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using MediatR;
using Rossetti.Common.ErrorHandler;
using Rossetti.Common.Result;
using Rossetti.Common.Validation;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

public class UpdateEstimateHandler : IRequestHandler<UpdateEstimateCommand, ResultOf<Operation>>
{
    private readonly IEstimateRepository _estimateRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEstimateHandler(
        IEstimateRepository estimateRepository,
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork)
    {
        _estimateRepository = estimateRepository;
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultOf<Operation>> Handle(UpdateEstimateCommand command, CancellationToken cancellationToken)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(command.EstimateId);
        var supplier = await _supplierRepository.FetchByIdAsync(command.SupplierId);

        var errors = Validator.New()
            .When(estimate is null, CommonError.NotFound<EstimateEn>())
            .When(supplier is null, CommonError.NotFound<Supplier>())
            .ReturnErrors();

        if (errors.Any())
            return errors;

        UpdateEstimateInfo(estimate!, command);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Updated;
    }

    private void UpdateEstimateInfo(
        EstimateEn estimate,
        UpdateEstimateCommand command)
    {
        estimate.AlterSupplier(command.SupplierId);
        estimate.AlterName(command.Name);
        _estimateRepository.Update(estimate);
    }
}