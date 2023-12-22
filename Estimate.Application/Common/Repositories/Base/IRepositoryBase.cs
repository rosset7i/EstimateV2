using Estimate.Domain.Entities.Base;

namespace Estimate.Application.Common.Repositories.Base;

public interface IRepositoryBase<TId, TEntity> where TEntity : Entity
{
    Task<TEntity?> FetchByIdAsync(TId id);
    Task<ICollection<TEntity>> FetchAllAsync();
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}