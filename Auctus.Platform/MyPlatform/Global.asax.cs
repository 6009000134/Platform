using MvcApplication.Infrastructure.Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyPlatform
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static List<string> AllowedUrls = new List<string>();
        /// <summary>
        /// 程序开始
        /// </summary>
        protected void Application_Start()
        {
            //允许通过的Options预检请求地址
            //AllowedUrls=System.Configuration.ConfigurationManager.AppSettings["AllowedOptions"].Split(new char[] { ';'}).ToList();
            foreach (string item in System.Configuration.ConfigurationManager.AppSettings["AllowedOptions"].Split(new char[] { ';' }).ToList())
            {
                if (item == "*")
                {
                    AllowedUrls.Insert(0, item);
                }
                else
                {
                    AllowedUrls.Add(item);
                }                
            }
            //加载log4net配置
            string path = AppContext.BaseDirectory + "log4net.config";
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(path));
            MyPlatform.Common.LogHelper.Default.LoadXmlConfig(path);

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new AreaHttpControllerSelector(GlobalConfiguration.Configuration));
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MyPlatform.Common.LogHelper.Default.WriteInfo("Application Start ");
            MyPlatform.Common.LogHelper.Default.WriteInfo("AllowedUrls Count: "+AllowedUrls.Count.ToString());
            //System.Timers.Timer cronJob = new System.Timers.Timer(1000);
            //string n = "Timer"+DateTime.Now.ToString();
            //cronJob.Elapsed += new System.Timers.ElapsedEventHandler((s,e)=>CronJob_Elapsed(s,e,n));
            //cronJob.Enabled = true;
            //cronJob.AutoReset = true;
            //cronJob.Start();
            DateTime dt = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
        }
        /// <summary>
        /// Application_BeginRequest
        /// </summary>
        protected void Application_BeginRequest()
        {
            //TODO:对应请求的拦截或者放行权限可以做的更详细
            //允许指定域名的Options预检请求通过
            Dictionary<string, string> dicHead = new Dictionary<string, string>();
            for (int i = 0; i < Request.Headers.AllKeys.Count(); i++)
            {
                dicHead.Add(Request.Headers.Keys[i], Request.Headers[i]);
            }
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS"&& IsAllowedOption(AllowedUrls))
            {
                Response.End();
            }
        }
        /// <summary>
        /// 程序关闭或IIS自动回收
        /// </summary>
        protected void Application_End()
        {
            // 程序关闭后等2s，给站点发送web请求，重新打开程序
            System.Threading.Thread.Sleep(2000);
            // 发送请求打开程序。
            // TODO: 优化：可以通过编写windows服务来完成此任务，而不是写在此处
            string url = "http://localhost:9001/Home/Index";
            System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流
        }
        /// <summary>
        /// 验证是否为允许的域名
        /// </summary>
        /// <param name="allowUrls">白名单集合</param>
        /// <returns></returns>
        private bool IsAllowedOption(List<string> allowUrls)
        {
            bool flag = true;
            if (allowUrls.Count>0)
            {
                if (allowUrls[0] == "*"|| allowUrls.Select(m => m.Contains(Request.UrlReferrer.Host)).Count() > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 计时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="extraPar">额外增加的参数</param>
        private void CronJob_Elapsed(object sender, System.Timers.ElapsedEventArgs e,string extraPar)
        {
            MyPlatform.Common.LogHelper.Default.WriteInfo("My Name is: "+ extraPar + "!----SignalTime: "+e.SignalTime.ToString());
            int mint = e.SignalTime.Minute;
            if (mint==0)//整点发送邮件
            {

            }
            //MyPlatform.Common.LogHelper.Default.WriteInfo(e.SignalTime.ToString());
        }
    }
}
