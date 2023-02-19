using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace NerRedist_Presentation.Extentions
{
    public static class DistributedCacheExtentions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absouluteExpierTime
            , TimeSpan? unUsedExpierTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absouluteExpierTime ?? TimeSpan.FromSeconds(120),
                SlidingExpiration = unUsedExpierTime,
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);

        }
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData is null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
