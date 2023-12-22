using Estimate.Application.Common.Repositories;
using Estimate.Domain.Entities.Estimate.Events;
using MediatR;

namespace Estimate.Application.Estimates.Events;

public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
{
    private readonly IEstimateRepository _estimateRepository;

    public ProductUpdatedEventHandler(IEstimateRepository estimateRepository)
    {
        _estimateRepository = estimateRepository;
    }

    public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product
    }
}