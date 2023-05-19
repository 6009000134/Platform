using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Common
{
    public static class MyExtends
    {
        /// <summary>
        /// 将指定的T类型对象转成Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T t)
        {
            return JSONUtil.GetJson<T>(t);
        }
        ///// <summary>
        ///// 将ReturnData转换成Json字符串
        ///// </summary>
        ///// <param name="result"></param>
        ///// <returns></returns>
        //public static string ToJson(this ReturnData result)
        //{
        //    return JSONUtil.GetJson(result);
        //}
        /// <summary>
        /// 字符换转换成Json
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToJson(this string str)
        {
            return JSONUtil.GetJson<string>(str);
        }        
    }
}
