using System;
using System.Web;

namespace MyPlatform.Common.Cache
{
    public class DataCache : ICache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetCache(string key)
        {
            System.Web.Caching.Cache c = HttpRuntime.Cache;
            return c.Get(key);
        }        
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCache(string key, object value)
        {            
            System.Web.Caching.Cache c = HttpRuntime.Cache;
            c.Insert(key, value);
        }
        /// <summary>
        /// 设置缓存（绝对过期时间）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        public void SetCache(string key, object value, DateTime absoluteExpiration)
        {
            System.Web.Caching.Cache c = HttpRuntime.Cache;
            c.Insert(key, value, null, absoluteExpiration, TimeSpan.Zero);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        public void SetCache(string key, object value, long timeSpan)
        {
            System.Web.Caching.Cache c = HttpRuntime.Cache;
            c.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromSeconds(timeSpan));
        }
    }
}
