using System;
using System.Web;
using System.Web.Caching;

namespace LianFa.ShopPlatform.Code.Cache
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="absoluteExpiration">缓存结束时间（绝对）</param>
        /// <param name="slidingExpiration">缓存结束时间差</param>
        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="cacheDpFilePath">缓存生存期依赖文件物理路径</param>
        public static void SetCache(string cacheKey, object objObject, string cacheDpFilePath)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, new CacheDependency(cacheDpFilePath));
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="timeOut">超时时间（秒）</param>
        public static void SetCache(string cacheKey, object objObject, int timeOut)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, DateTime.Now.AddSeconds(timeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void ClearCahe(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }
    }
}
