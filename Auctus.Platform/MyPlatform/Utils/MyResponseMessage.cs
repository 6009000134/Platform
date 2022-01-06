using MyPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace MyPlatform.Utils
{
    /// <summary>
    /// 响应消息
    /// </summary>
    public static class MyResponseMessage
    {
        const string jsonContentType = "application/json";//Content-Type
        #region Extend your function

        #endregion
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">JSON对象</param>
        /// <returns></returns>
        public static HttpResponseMessage SuccessJson<T>(T t)
        {
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(t.ToJson<T>(), System.Text.Encoding.UTF8, jsonContentType) };
        }
        /// <summary>
        /// 返回Json数据
        /// </summary>
        /// <param name="result">ReturnData对象</param>
        /// <returns></returns>
        public static HttpResponseMessage SuccessJson(Model.ReturnData result)
        {
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(result.ToJson(), System.Text.Encoding.UTF8, jsonContentType) };
        }
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="header"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static HttpResponseMessage SuccessJson<T>(Dictionary<string,string> header, T t)
        {
            HttpResponseMessage result= new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = new StringContent(t.ToJson<T>(), System.Text.Encoding.UTF8, jsonContentType) };
            for (int i = 0; i < header.Count(); i++)
            {                
                result.Headers.Add(header.ElementAt(i).Key, header.ElementAt(i).Value);
            }
            return result;
        }
        /// <summary>
        /// 未授权
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static HttpResponseMessage UnAuthorized(string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                errorMsg = "未授权，请重新登录或联系管理员！";
            }
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized, Content = new StringContent(errorMsg.ToJson(), System.Text.Encoding.UTF8, jsonContentType) };
        }

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        /// <param name="errorMsg">异常信息</param>
        /// <returns></returns>
        public static HttpResponseMessage InternalServerError(string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                errorMsg = "服务器内部错误，请联系管理员！";
            }
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError, Content = new StringContent(errorMsg.ToJson(), System.Text.Encoding.UTF8, jsonContentType) };
        }

        public static HttpResponseMessage Forbidden(string errorMsg)
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                errorMsg = "未经授权，禁止访问！若有疑问，请联系管理员！";
            }
            return new HttpResponseMessage() { StatusCode = HttpStatusCode.Forbidden, Content = new StringContent(errorMsg.ToJson(), System.Text.Encoding.UTF8, jsonContentType) };
        }

        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="statusCode">状态码</param>
        /// <param name="header">自定义http header</param>
        /// <param name="json">json字符创</param>
        /// <returns></returns>
        public static HttpResponseMessage ResponseJSON(HttpStatusCode statusCode,Dictionary<string,string> header, string json)
        {
            HttpResponseMessage result = new HttpResponseMessage() { StatusCode = statusCode, Content = new StringContent(json, System.Text.Encoding.UTF8, jsonContentType) };
            for (int i = 0; i < header.Count(); i++)
            {
                result.Headers.Add(header.ElementAt(i).Key, header.ElementAt(i).Value);
            }
            return result;
        }        
    }
}