using Estimate.Application.Estimates.Dtos;
using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.UpdateEstimateUseCase;

public class UpdateEstimateHandler
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
    
    public async Task UpdateEstimateInfoAsync(
        Guid estimateId,
        UpdateEstimateInfoRequest request)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(estimateId);
        var supplier = await _supplierRepository.FetchByIdAsync(request.SupplierId);

        Validator.New()
            .When(estimate is null, DomainError.Common.NotFound<EstimateEn>())
            .When(supplier is null, DomainError.Common.NotFound<Supplier>())
            .ThrowExceptionIfAny();

        UpdateEstimateInfo(estimate!, request);
        await _unitOfWork.SaveChangesAsync();
    }
    
    private void UpdateEstimateInfo(
        EstimateEn estimate,
        UpdateEstimateInfoRequest request)
    {
        var updateEstimate = request.UpdateInfoOf(estimate);
        _estimateRepository.Update(updateEstimate);
    }
}