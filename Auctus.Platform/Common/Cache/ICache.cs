using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPlatform.Common.Cache
{
    interface ICache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetCache(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void SetCache(string key, object value);
        /// <summary>
        /// 设置缓存（绝对过期时间）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        void SetCache(string key, object value, DateTime absoluteExpiration);
        /// <summary>
        /// 设置缓存（相对过期时间）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan">秒</param>
        void SetCache(string key, object value, long timeSpan);
    }
}
