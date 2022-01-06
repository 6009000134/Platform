using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace MyPlatform.Filter
{
    /// <summary>
    /// 捕获全局异常并记录日至
    /// </summary>
    public class MyExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            // 记录日志
            Exception ex = actionExecutedContext.Exception;
            MyPlatform.Common.LogHelper.Default.WriteError(ex.Message.ToString(), ex);
            //返回异常信息
            ReturnData result = new ReturnData();
            result.S = false;
            result.M = ex.Message.ToString();
            //actionExecutedContext.Response = MyResponseMessage.InternalServerError(ex.Message.ToString());
            actionExecutedContext.Response = MyResponseMessage.SuccessJson(result);
        }
    }
}