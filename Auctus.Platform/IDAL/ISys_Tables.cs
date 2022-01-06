using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.IDAL {
	/// <summary>
	/// 接口层Sys_Tables
	/// </summary>
	public interface ISys_Tables
	{
        #region Extend By liufei
        DataTable GetListByDBName(string DBName);
        DataTable GetListByDBName(Dictionary<string, object> dicCondition);
        bool Exists(string dbName);
        ReturnData ExistsTable(IDataBase dbCon, string tableName);
        ReturnData Add(MyPlatform.Model.Sys_TablesModel model);
        bool Edit(MyPlatform.Model.Sys_TablesModel model);
        bool Delete(int tableID );
        ReturnData GetDetail(int tableID, MyPlatform.Model.Pagination page);
        MyPlatform.Model.Sys_TablesModel GetModel(IDataBase db);
        DataSet GetSysTableList(IDataBase db);
        DataSet GetSysTableByName(IDataBase db,string tableName);
        bool SyncTaleInfo(IDataBase db, Dictionary<string, string> dbInfo, DataSet ds);
        #endregion
    }
}