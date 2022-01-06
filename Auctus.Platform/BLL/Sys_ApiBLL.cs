using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

namespace MyPlatform.BLL
{
    public class Sys_ApiBLL : BLLBase
    {
        //TODO:做个基类，用来决定使用哪个DBHelper，声明对应dal
        string defaultCon = "Default";
        private readonly ISys_Api dal = DataAccess.CreateInstance<ISys_Api>("Sys_ApiDAL");
        public Sys_ApiBLL()
        {
            GetDataBase(defaultCon);
        }
        //根据apiid，获取相关表的所有数据
        /// <summary>
        /// 获取以ApiName为表名的表数据
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        public DataSet GetApiData(int apiID)
        {
            DataSet ds = new DataSet();
            try
            {
                Model.Sys_APIModel apiInfo = GetApiInfo(apiID);
                ds = dal.GetDataByApiName(currentDB,apiInfo.ApiName);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return ds;
        }
            
        public DataSet GetTsCode(string tsCode)
        {
            //IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);            
            return dal.GetTsCode(currentDB, tsCode);
        }
        public Model.Sys_APIModel GetApiInfo(int apiID)
        {
            Model.Sys_APIModel apiInfo = dal.GetApiInfo(currentDB, apiID);
            if (apiInfo == null)
            {
                throw new Exception("找不到API信息");
            }
            return apiInfo;
        }
        public ReturnData GetApiResult(TuShareResult data, int apiID)
        {
            //IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
            return dal.GetApiResult(currentDB, data, apiID);
        }
        public ReturnData CreateApiTable(int apiID)
        {
            //IDataBase db = DBHelperFactory.CreateDBInstance(defaultCon);
            return dal.CreateApiTable(currentDB, apiID);
        }
        public DataSet GetDetail(int apiID)
        {
            return dal.GetDetail(apiID);
        }
        /// <summary>
        /// 查询api信息
        /// </summary>
        /// <param name="condition">标题、api名称、描述关键字</param>
        /// <returns></returns>
        public DataSet GetList(string condition)
        {
            return dal.GetList(condition);
        }
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public ReturnData Add(Dictionary<object, object> dic)
        {
            return dal.Add(dic);
        }
        public DataTable GetNoDataCalendar(string exchange)
        {
            return dal.GetNoDataCalendar(currentDB, exchange);
        }
    }
}
