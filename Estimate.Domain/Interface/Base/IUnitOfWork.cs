namespace Estimate.Domain.Interface.Base;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}