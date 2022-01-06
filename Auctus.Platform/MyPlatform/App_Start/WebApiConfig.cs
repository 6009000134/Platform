using MyPlatform.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyPlatform
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{area}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }                
            );

            //自定义全局异常处理
            config.Filters.Add(new MyExceptionAttribute());
            config.Filters.Add(new ApiAuthorization());
        }
    }
}
