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
    //Sys_Tables
    public partial class Sys_TablesBLL:BLLBase
    {
        //private string defaultCon = "Default";
        //private readonly ISys_Tables dal = DataAccess.CreateInstance<ISys_Tables>("Sys_TablesDAL");
        ISys_Tables dal;// = DALFactory.DataAccess.CreateInstance<IQueryObject>("QueryObjectDAL");
        /// <summary>
        /// 对应DAL名称
        /// </summary>
        //public override string DalName
        //{
        //    get
        //    {
        //        return "Sys_TablesDAL";
        //    }
        //}
        public Sys_TablesBLL()
        {
            //TODO:CreateInstance是否会创建BLLBase的实例
            dal = this.CreateInstance<ISys_Tables>();
        }
        #region extend

        public bool Delete(int tableID)
        {
            ////判断是否存在表
            //IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            //Model.Sys_Tables tableInfo = dal.GetModel(db);
            //if (tableInfo==null)
            //{
            //    throw new Exception("未找到表信息，无法删除！");
            //}
            //ReturnData result = ExistsTable(tableInfo.DBCon, tableInfo.TableName);
            //if (result.S)
            //{
            //    if (tableInfo.DBCon==defaultCon)
            //    {

            //    }
            //    IDataBase acDB = DBHelperFactory.Create(tableInfo.DBCon);
            //}            
            return dal.Delete(tableID);
        }
        public DataTable GetListByDBName(string DBName)
        {
            return dal.GetListByDBName(DBName);
        }

        public bool Exists(string tableName, string dbName, string dbTypeCode)
        {
            return dal.Exists(dbName);
        }
        /// <summary>
        /// 判断数据库中是否存在同名表
        /// </summary>
        /// <param name="dbCon">数据库名</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public ReturnData ExistsTable(string dbCon, string tableName)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(dbCon);
            if (db == null)
            {
                throw new Exception("数据库" + dbCon + "连接错误！");
            }
            return dal.ExistsTable(db, tableName);
        }
        /// <summary>
        /// 新增表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Add(MyPlatform.Model.Sys_TablesModel model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 编辑表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyPlatform.Model.Sys_TablesModel model)
        {
            return dal.Edit(model);
        }
        /// <summary>
        /// 获取表详情（表信息及列信息）
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ReturnData GetDetail(int tableID, Pagination page)
        {
            return dal.GetDetail(tableID, page);
        }
        /// <summary>
        /// 获取数据库sysobjects信息
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        public DataSet GetSysTableList(string dbCon)
        {
            IDataBase db = DBHelperFactory.Create(dbCon);
            return dal.GetSysTableList(db);
        }
        public ReturnData SyncTableInfo(string dbCon, string tableName)
        {
            ReturnData result = new ReturnData();
            try
            {
                //获取数据库表信息
                IDataBase db = DBHelperFactory.Create(dbCon);
                DataSet ds=dal.GetSysTableByName(db, tableName);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    result.SetErrorMsg("数据库找不到名为[" + tableName + "]的表");
                }
                else
                {
                    DBUtility.DBHelperBase d = new DBHelperBase();
                    result.S=dal.SyncTaleInfo(db, d.GetDBInfo(dbCon), ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}