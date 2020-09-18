using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using CacheItemPriority = System.Web.Caching.CacheItemPriority;

namespace LianFa.ShopPlatform.Code.Cache
{
    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static readonly object _locker = new object();

        public static T GetCacheItem<T>(string key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null) throw new ArgumentNullException(nameof(cachePopulate));
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[key] == null)
            {
                lock (_locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = (System.Runtime.Caching.CacheItemPriority)CacheItemPriority.Default;

            return policy;
        }
    }
}
