using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Data;
namespace MyPlatform.IDAL
{
    /// <summary>
    /// 接口层Sys_Columns
    /// </summary>
    public interface ISys_Columns
    {
        #region  成员方法
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(int tableID);
        ReturnData Add(IDataBase db,Model.Sys_ColumnsModel model);
        #endregion  成员方法
    }
}