using System.Web.Mvc;

namespace MyPlatform.Areas.Web
{
    /// <summary>
    /// Web Area
    /// </summary>
    public class WebAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// 
        /// </summary>
        public override string AreaName 
        {
            get 
            {
                return "Web";
            }
        }
        /// <summary>
        /// 注册WebArea
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Web_default",
                "Web/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}