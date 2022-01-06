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
    /// 列操作
    /// </summary>
    public class ColumnController : ApiController
    {
        MyPlatform.BLL.Sys_ColumnsBLL colBLL = new MyPlatform.BLL.Sys_ColumnsBLL();
        /// <summary>
        /// 获取表的列信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage List([FromBody]int tableID)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = true;
                result.D=colBLL.GetList(tableID).Tables[0];
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("获取表字段失败：" + ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }

        /// <summary>
        /// 新增列
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddColumn([FromBody]Sys_ColumnsModel columns)
        {
            ReturnData result = new ReturnData();
            try
            {
                colBLL.AddColumn(columns);
                //string s = "";
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("新增列失败：" + ex.Message);
                //throw;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="jsonStr">Json字符串格式:{DBName:"db1",TableName:"tb1",Columns["col1","col2","col3"]}</param>
        /// <returns></returns>
        public HttpResponseMessage DeleteColumns([FromBody]string jsonStr)
        {
            ReturnData result = new ReturnData();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic = JSONUtil.ParseFromJson<Dictionary<string, object>>(jsonStr);
            
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

    }
}
