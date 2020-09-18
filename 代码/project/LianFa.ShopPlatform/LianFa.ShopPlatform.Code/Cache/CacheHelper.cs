using System;
using System.Web;
using System.Web.Caching;

namespace LianFa.ShopPlatform.Code.Cache
{
    /// <summary>
    /// ���������
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="cacheKey">�����</param>
        /// <param name="objObject">�������</param>
        /// <param name="absoluteExpiration">�������ʱ�䣨���ԣ�</param>
        /// <param name="slidingExpiration">�������ʱ���</param>
        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="cacheDpFilePath">���������������ļ�����·��</param>
        public static void SetCache(string cacheKey, object objObject, string cacheDpFilePath)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, new CacheDependency(cacheDpFilePath));
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="timeOut">��ʱʱ�䣨�룩</param>
        public static void SetCache(string cacheKey, object objObject, int timeOut)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, DateTime.Now.AddSeconds(timeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void ClearCahe(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }
    }
}
