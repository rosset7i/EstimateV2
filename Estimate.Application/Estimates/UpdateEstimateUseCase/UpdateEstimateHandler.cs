using Estimate.Domain.Common;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

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
            .When(estimate is null, DomainError.Common.NotFound<EstimateEn>())
            .When(supplier is null, DomainError.Common.NotFound<Supplier>())
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
        var updateEstimate = command.UpdateInfoOf(estimate);
        _estimateRepository.Update(updateEstimate);
    }
}