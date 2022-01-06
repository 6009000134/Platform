using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;

namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuController : ApiController
    {
        MyPlatform.BLL.Sys_MenuBLL menuBLL = new MyPlatform.BLL.Sys_MenuBLL();
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add([FromBody]MyPlatform.Model.Sys_MenuModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = menuBLL.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception("添加菜单失败：" + ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        public HttpResponseMessage Edit([FromBody]MyPlatform.Model.Sys_MenuModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = true;
            }
            catch (Exception ex)
            {
                throw new Exception("修改菜单失败："+ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetMenuTree()
        {
            ReturnData result = new ReturnData();
            try
            {
                result = menuBLL.GetMenuTree();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
    }
}
