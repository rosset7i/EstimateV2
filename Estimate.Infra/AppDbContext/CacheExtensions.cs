using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Estimate.Infra.AppDbContext;

public static class CacheExtensions
{
    public static async Task SetRecordAsync<TData>(
        this IDistributedCache cache,
        string id,
        TData data,
        TimeSpan? expireDate = null)
    {
        var option = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expireDate ?? TimeSpan.FromSeconds(90)
        };

        var serializeOptions = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        await cache.SetStringAsync(id, JsonConvert.SerializeObject(data, serializeOptions), option);
    }

    public static async Task<TData?> GetRecordAsync<TData>(
        this IDistributedCache cache,
        string id)
    {
        var cachedEntity = await cache.GetStringAsync(id);

        return cachedEntity is null
            ? default
            : JsonConvert.DeserializeObject<TData>(cachedEntity);
    }
}