using Estimate.Application.Common.Repositories.Base;
using Estimate.Domain.Entities.Base;
using Estimate.Infra.AppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Estimate.Infra.Repositories.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly EstimateDbContext _dbContext;
    private readonly IPublisher _publisher;

    public UnitOfWork(
        EstimateDbContext dbContext,
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task SaveChangesAsync()
    {
        var domainEvents = _dbContext.ChangeTracker.Entries<Entity>()
            .SelectMany(entry => entry.Entity.PopDomainEvents())
            .ToList();

        await PublishDomainEvents(domainEvents);
        await _dbContext.SaveChangesAsync();
    }

    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent);
    }
}