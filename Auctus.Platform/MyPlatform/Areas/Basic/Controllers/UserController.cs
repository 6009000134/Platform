using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 用户控制
    /// </summary>
    public class UserController : ApiController
    {
        MyPlatform.BLL.Sys_UsersBLL userBLL = new MyPlatform.BLL.Sys_UsersBLL();
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage List([FromBody]dynamic obj)
        {
            ReturnData result = new ReturnData();
            try
            {
                List<Dictionary<string, string>> condition = JSONUtil.ParseFromJson<List<Dictionary<string, string>>>(Convert.ToString(obj.condition));
                Pagination page = JSONUtil.ParseFromJson<Pagination>(Convert.ToString(obj.page));
                result.D = userBLL.GetList(condition, page);
                result.S = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add(Model.Sys_UsersModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                userBLL.Validate(model);
                if (userBLL.Add(model) > 0)
                {
                    result.SetSuccessMsg("创建成功！");
                }
                else
                {
                    result.SetErrorMsg("创建失败！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Detail([FromBody]int userID)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.D = userBLL.GetUserByID(userID);
                result.S = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="model">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Edit([FromBody]Sys_UsersModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.D = userBLL.Edit(model);
                result.S = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }

    }
}
