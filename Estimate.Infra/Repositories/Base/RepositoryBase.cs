using Estimate.Domain.Entities.Base;
using Estimate.Domain.Interface.Base;
using Estimate.Infra.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Estimate.Infra.Repositories.Base;

public abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity> where TEntity : Entity
{
    protected readonly EstimateDbContext DbContext;
    protected readonly IDistributedCache DistributedCache;

    protected RepositoryBase(
        EstimateDbContext dbContext,
        IDistributedCache distributedCache)
    {
        DbContext = dbContext;
        DistributedCache = distributedCache;
    }

    public async Task<TEntity?> FetchByIdAsync(TId id)
    {
        var key = $"{typeof(TEntity)} - {id}";

        var cachedEntity = await DistributedCache
            .GetRecordAsync<TEntity>(key);

        if (cachedEntity is null)
        {
            var entity = await DbContext.Set<TEntity>()
                .FindAsync(id);

            if (entity is null)
                return entity;

            await DistributedCache.SetRecordAsync(key, entity);

            return entity;
        }

        DbContext.Set<TEntity>()
            .Attach(cachedEntity);

        return cachedEntity;
    }

    public async Task<ICollection<TEntity>> FetchAllAsync() =>
        await DbContext.Set<TEntity>().ToListAsync();

    public async Task AddAsync(TEntity entity) =>
        await DbContext.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity)
    {
        RemoveFromCache(entity);

        DbContext.Set<TEntity>().Update(entity);
    }
    
    public void Delete(TEntity entity) =>
        DbContext.Set<TEntity>().Remove(entity);

    private void RemoveFromCache(TEntity entity) =>
        DistributedCache.Remove(GetKey(entity));

    private string GetKey(TEntity entity) =>
        $"{typeof(TEntity)} - {entity.Id}";
}