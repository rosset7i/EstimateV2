using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public class RemoveEstimateHandler
{
    private readonly IEstimateRepository _estimateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveEstimateHandler(
        IEstimateRepository estimateRepository,
        IUnitOfWork unitOfWork)
    {
        _estimateRepository = estimateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteEstimateByIdAsync(Guid estimateId)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(estimateId);

        if (estimate is null)
            throw new BusinessException(DomainError.Common.NotFound<EstimateEn>());

        _estimateRepository.Delete(estimate);
        await _unitOfWork.SaveChangesAsync();
    }
}