using Estimate.Domain.Common;
using Estimate.Domain.Entities;
using Estimate.Domain.Interface;
using Estimate.Domain.Interface.Base;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public class RemoveEstimateHandler : IRequestHandler<RemoveEstimateCommand, RemoveEstimateResult>
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

    public async Task<RemoveEstimateResult> Handle(RemoveEstimateCommand command, CancellationToken cancellationToken)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(command.EstimateId);

        if (estimate is null)
            throw new BusinessException(DomainError.Common.NotFound<EstimateEn>());

        _estimateRepository.Delete(estimate);
        await _unitOfWork.SaveChangesAsync();

        return new RemoveEstimateResult();
    }
}