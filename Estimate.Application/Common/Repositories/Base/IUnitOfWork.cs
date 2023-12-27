namespace Estimate.Application.Common.Repositories.Base;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}