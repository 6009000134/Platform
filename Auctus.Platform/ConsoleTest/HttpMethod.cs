﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleTest
{
    public class HttpMethod
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受   
        }

        public static HttpWebResponse CreatePostHttpResponse(string url, string json, Encoding charset)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            //request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";        
            byte[] data = Encoding.UTF8.GetBytes(json);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        public static string PostMethod(string url, string json)
        {
            // System.Text.Encoding.UTF8
            Encoding encoding = System.Text.Encoding.UTF8;
            HttpWebResponse response = HttpMethod.CreatePostHttpResponse(url, json, encoding);
            
            //打印返回值
            Stream stream = response.GetResponseStream();   //获取响应的字符串流
            using (StreamReader sr = new StreamReader(stream,encoding))//创建一个stream读取流
            {    
                string html =HttpUtility.UrlDecode(sr.ReadToEnd());   //从头读到尾，放到字符串html
                return html;
            }
        }

        public static string PostMethod(string url, Dictionary<string, string> dicPararm)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            HttpWebResponse response = HttpMethod.CreatePostHttpResponse(url, dicPararm, encoding);
            //打印返回值
            Stream stream = response.GetResponseStream();   //获取响应的字符串流
            using (StreamReader sr = new StreamReader(stream))//创建一个stream读取流
            {
                string html = sr.ReadToEnd();   //从头读到尾，放到字符串html
                return html;
            }
        }

        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = DefaultUserAgent;
            request.Proxy = null;
            //如果需要POST数据   
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = charset.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

    }
}
