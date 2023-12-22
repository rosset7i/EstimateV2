using Estimate.Application.Common.Repositories;
using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Common;
using Estimate.Domain.Common.CommonResults;
using Estimate.Domain.Common.Errors;
using Estimate.Domain.Entities;
using Estimate.Domain.Entities.Estimate;
using MediatR;
using DomainError = Estimate.Domain.Common.Errors.DomainError;

namespace Estimate.Application.Estimates.RemoveEstimateUseCase;

public class RemoveEstimateHandler : IRequestHandler<RemoveEstimateCommand, ResultOf<Operation>>
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

    public async Task<ResultOf<Operation>> Handle(RemoveEstimateCommand command, CancellationToken cancellationToken)
    {
        var estimate = await _estimateRepository.FetchByIdAsync(command.EstimateId);

        if (estimate is null)
            return DomainError.Common.NotFound<EstimateEn>();

        _estimateRepository.Delete(estimate);
        await _unitOfWork.SaveChangesAsync();

        return Operation.Deleted;
    }
}