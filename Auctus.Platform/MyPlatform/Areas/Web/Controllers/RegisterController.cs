using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPlatform.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MyPlatform.BLL;
using MyPlatform.Common;
using MyPlatform.Utils;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// 注册
    /// </summary>
    public class RegisterController : ApiController
    {
        MyPlatform.BLL.Sys_UsersBLL userBLL = new MyPlatform.BLL.Sys_UsersBLL();
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="model">用户注册信息Json字符串</param>
        /// <returns></returns>
        [HttpPost]
        //public HttpResponseMessage Register([FromBody]string json)
        public HttpResponseMessage Register(MyPlatform.Model.Sys_UsersModel model)
        {
        //    MyPlatform.Model.Sys_Users model = MyPlatform.Common.JSONUtil.ParseFromJson<MyPlatform.Model.Sys_Users>(json);
            ReturnData returnData = new ReturnData();//返回数据            
            try
            {
                

                userBLL.Validate(model);//数据校验
                string pinyin=Common.PinYinConverter.GetPinYin(model.UserName);
                if (string.IsNullOrEmpty(model.CreatedBy))
                {
                    model.CreatedBy = model.UserName;
                }
                model.CreatedDate = DateTime.Now;
                if (userBLL.Exists(model.Account))//判断账号是否已注册
                {
                    returnData.SetErrorMsg("账号已被注册");
                }
                else//账号未被注册，进行新增操作
                {
                    if (userBLL.Add(model) > 0)
                    {
                        returnData.SetSuccessMsg("创建成功!");
                    }
                    else
                    {
                        returnData.SetErrorMsg("创建失败!");
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(returnData);
        }
    }
}
