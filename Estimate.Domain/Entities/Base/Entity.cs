namespace Estimate.Domain.Entities.Base;

public abstract class Entity
{
    public Guid Id { get; set; }
    protected readonly List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}