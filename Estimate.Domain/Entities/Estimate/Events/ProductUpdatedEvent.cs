using Estimate.Domain.Entities.Base;

namespace Estimate.Domain.Entities.Estimate.Events;

public class ProductUpdatedEvent : IDomainEvent
{
    public Guid ProductId { get; set; }
}