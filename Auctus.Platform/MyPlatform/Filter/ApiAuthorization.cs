using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyPlatform.Filter
{
    /// <summary>
    /// WebAPI授权验证
    /// </summary>
    public class ApiAuthorization: AuthorizationFilterAttribute
    {
        /// <summary>
        /// 用户权限验证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            //return;
            // 对AllowAnonymousAttribute的Action不进行校验
            string s = actionContext.RequestContext.Url.Request.Headers.Referrer.ToString();
            string ss = actionContext.Request.Headers.Referrer.ToString();
            string sss=actionContext.Request.Headers.Referrer.AbsolutePath;
            string ssss = actionContext.Request.Headers.Referrer.AbsoluteUri;
            Uri u = actionContext.Request.Headers.Referrer;
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            // 校验token和权限
            string token = "";
            IEnumerable<string> iToken;
            if (actionContext.Request.Headers.TryGetValues("Token", out iToken))
            {
                //TODO:可以设置访问白名单或者所有用户添加访问权限

                token = iToken.First();
                //TODO:校验token，获取token
                Dictionary<string, object> validateResult = Common.JWTTokenHelper.ValidateToken(token);
                if (Convert.ToBoolean(validateResult["S"]))
                {                    
                    //TODO: 验证是否有权限
                    bool IsAuthorized = true;
                    
                    if (IsAuthorized)
                    {
                        //actionContext.ControllerContext.Request.Properties
                        base.OnAuthorization(actionContext);
                    }
                    else
                    {
                        actionContext.Response = MyPlatform.Utils.MyResponseMessage.UnAuthorized("访问的模块未授权，请联系管理员！");
                    }
                }
                else
                {
                    actionContext.Response = MyPlatform.Utils.MyResponseMessage.UnAuthorized(validateResult["M"].ToString());
                }
            }
            else
            {
                actionContext.Response=MyPlatform.Utils.MyResponseMessage.UnAuthorized("没有Token令牌，请重新登录或联系管理员！");
            }
        }
    }
}