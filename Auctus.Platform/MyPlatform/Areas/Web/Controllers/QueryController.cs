using MyPlatform.BLL;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// 通用查询功能
    /// </summary>
    public class QueryController : ApiController
    {
        private QueryBLL bll = new QueryBLL();
        /// <summary>
        /// 查询视图数据集合
        /// </summary>
        /// <param name="dic">{ID:'查询视图的ID',page:{PageSize:1,PageIndex:10,TotalCount:123,PageCount:1}}</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage List([FromBody]Dictionary<string, object> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                // Dictionary<string, object> dic = JSONUtil.ParseFromJson<Dictionary<string, object>>(queryStr);
                if (!dic.ContainsKey("ID"))
                {
                    throw new Exception("未找到ID参数！");
                }
                int id = Convert.ToInt32(dic["ID"]);
                Pagination page;
                if (!dic.ContainsKey("page"))
                {
                    page = JSONUtil.ParseFromJson<Pagination>(dic["page"].ToString());
                }
                //根据ID获取对象信息
                QueryObjectModel queryObj = bll.GetQueryObjectData(id);
                DataSet ds= bll.GetList(queryObj);
                Dictionary<string, object> dicResult = new Dictionary<string, object>(); ;
                dicResult.Add("Cols",queryObj);
                dicResult.Add("Data",ds.Tables[0]);
                result.D = dicResult;
                result.S = true;
            }
            catch (Exception ex)
            {
                ex.Source += "QueryControll List";
                throw ex;
            }

            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 查询视图
        /// </summary>
        /// <param name="id">视图ID</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Query([FromBody]int id)
        {
            ReturnData result = new ReturnData();
            try
            {
                //根据ID获取对象信息                
                //根据ID获取对象信息
                QueryObjectModel queryObj = bll.GetQueryObjectData(id);
                DataSet ds = bll.GetList(queryObj);
                Dictionary<string, object> dicResult = new Dictionary<string, object>(); ;
                dicResult.Add("Cols", queryObj);
                dicResult.Add("Data", ds.Tables[0]);
                result.D = dicResult;
                result.S = true;
            }
            catch (Exception ex)
            {
                ex.Source += "QueryControll List";
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
    }
}
