using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class HttpHelper
    {
        /// <summary>
        /// 获取或设置请求的身份验证信息
        /// </summary>
        public string Credentials { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否与 Internet 资源建立持久性连接
        /// </summary>
        public string KeepAlive { get; set; }
        /// <summary>
        /// 获取或设置请求的代理信息。
        /// </summary>
        public string Proxy { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否将数据分段发送到 Internet 资源。
        /// </summary>
        public string SendChunked { get; set; }

        /// <summary>
        /// 获取或设置 User-agent HTTP 标头的值
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// 获取或设置请求的超时值。
        /// </summary>
        private int timeOut = 60000;
        /// <summary>
        /// 格式类型
        /// </summary>
        private string contentType = "application/json;charset=UTF-8";
        /// <summary>
        /// encoding格式
        /// </summary>
        private string charset;
        public int TimeOut
        {
            get
            {
                return timeOut;
            }

            set
            {
                timeOut = value;
            }
        }
        public string ContentType
        {
            get
            {
                return contentType;
            }

            set
            {
                contentType = value;
            }
        }

        public string Charset
        {
            get
            {
                return charset;
            }

            set
            {
                charset = value;
            }
        }
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public string Get(string url, string body)
        {
            HttpWebRequest request = CreateRequest(url, body, "Get");
            return GetResponse(request);
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public string Post(string url, string body)
        {
            HttpWebRequest request = CreateRequest(url, body, "Post");
            return GetResponse(request);
        }
        /// <summary>
        /// 创建Request请求(UTF8编码)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private HttpWebRequest CreateRequest(string url, string body, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = ContentType;
            request.Timeout = TimeOut;
            byte[] bodys;
            
            bodys=Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodys.Length;
            request.GetRequestStream().Write(bodys, 0, bodys.Length);
            return request;
        }
        /// <summary>
        /// 获取Response结果字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetResponse(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string responseStr = sr.ReadToEnd();
            response.Close();
            sr.Close();
            request.Abort();
            return responseStr;
        }


    }
}
