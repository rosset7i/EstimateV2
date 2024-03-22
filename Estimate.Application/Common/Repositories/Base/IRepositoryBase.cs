using Rossetti.Common.Entities;

namespace Estimate.Application.Common.Repositories.Base;

public interface IRepositoryBase<TId, TEntity> where TEntity : Entity
{
    Task<TEntity?> FetchByIdAsync(TId id);
    Task<ICollection<TEntity>> FetchAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}