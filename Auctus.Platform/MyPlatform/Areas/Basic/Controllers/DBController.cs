using MyPlatform.Model.Enum;
using MyPlatform.Common.Cache;
using MyPlatform.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPlatform.Common;
using System.Xml;

namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public class DBController : ApiController
    {
        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage List()
        {
            ReturnData result = new ReturnData();
            try
            {
                //TODO:ConnectionString会传到前端，需要修正
                //Dictionary<string, string> dicDBList = new Dictionary<string, string>();
                //List<KeyValueData> li = new List<KeyValueData>();
                DataCache cache = new DataCache();
                object DBList = cache.GetCache("Sys_DBList2");
                if (DBList == null)
                {
                    result.D = DBInfoCache.GetDBListWithoutConStr();
                }
                else
                {
                    result.D = DBList;
                }
                result.S = true;
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("获取数据库信息失败：" + ex.Message);
            }

            return MyPlatform.Utils.MyResponseMessage.SuccessJson<ReturnData>(result);
        }


    }
}
