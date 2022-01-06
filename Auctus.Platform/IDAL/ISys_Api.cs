using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.IDAL
{
    public interface ISys_Api
    {
        Model.Sys_APIModel GetApiInfo(IDataBase db, int apiID);
        /// <summary>
        /// 获取以ApiName为表名的表数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="apiName"></param>
        /// <returns></returns>
        DataSet GetDataByApiName(IDataBase db,string apiName);
        /// <summary>
        /// 获取API详情
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        DataSet GetDetail(int apiID);
        /// <summary>
        /// 查询API信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataSet GetList(string condition);
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnData Add(Dictionary<object,object>dic);
        /// <summary>
        /// 删除API
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        ReturnData Delete(int apiID);
        /// <summary>
        /// 修改API信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ReturnData Edit(Sys_APIModel model);
        ReturnData CreateApiTable(IDataBase db, int apiID);
        ReturnData GetApiResult(IDataBase db, TuShareResult data, int apiID);
        DataSet GetTsCode(IDataBase db, string tsCode);
        DataTable GetNoDataCalendar(IDataBase db,string exchange);
    }
}
