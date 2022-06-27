using MyPlatform.Common;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : ApiController
    {
        MyPlatform.BLL.Sys_UsersBLL userBLL = new MyPlatform.BLL.Sys_UsersBLL();
        MyPlatform.BLL.Sys_TablesBLL bll = new BLL.Sys_TablesBLL();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login(Sys_UsersModel model)
        {
            ReturnData result = new ReturnData();
            IEnumerable<string> iToken;
            ActionContext.Request.Headers.TryGetValues("Token", out iToken);
            string token = iToken == null ? "" : iToken.First();
            HttpResponseMessage rep = new HttpResponseMessage();

            if (userBLL.Exists(model))//校验用户账号密码
            {
                //TODO:       2、获取用户权限，将token和权限键值对缓存 3、将菜单目录、权限返回前端                
                result.S = true;
                MyPlatform.Model.Sys_UsersModel user = userBLL.GetModelByAccount(model.Account);
                result.D = user;
                //生成Token
                Dictionary<string, object> dicPayload = new Dictionary<string, object>();
                dicPayload.Add("Account", user.Account);
                dicPayload.Add("UserName", user.UserName);
                token = Common.JWTTokenHelper.GenerateToken(dicPayload, Common.JWTTokenHelper.SetTimeOut(1));
            }
            else
            {
                result.SetErrorMsg("账号/密码错误！");
            }
            rep = MyPlatform.Utils.MyResponseMessage.SuccessJson(result);
            rep.Headers.Add("Access-Control-Expose-Headers", "Token");
            rep.Headers.Add("Token", token);
            return rep;
        }
        //TODO:校验token过期时，通过refresh_token刷新是否存在bug
        public HttpResponseMessage ValidateToken()
        {
            ReturnData result = new ReturnData();
            IEnumerable<string> iToken;
            ActionContext.Request.Headers.TryGetValues("Token", out iToken);
            string token = iToken == null ? "" : iToken.First();
            HttpResponseMessage rep = new HttpResponseMessage();
            bool isTokenEffective = false;
            if (!string.IsNullOrEmpty(token))//有token，校验token正确性
            {
                Dictionary<string, object> validateResult = Common.JWTTokenHelper.ValidateToken(token);
                if (Convert.ToBoolean(validateResult["S"]))
                {
                    isTokenEffective = true;
                }
            }
            else
            {

            }
            return rep;
        }
    }
}
