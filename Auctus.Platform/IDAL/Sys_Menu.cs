using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.IDAL
{
    /// <summary>
    /// 接口层Sys_Menu
    /// </summary>
    public interface ISys_Menu
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool Add(MyPlatform.Model.Sys_MenuModel model, IDataBase db);

        DataSet GetMenuTree(IDataBase db);
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool Edit(MyPlatform.Model.Sys_MenuModel model, IDataBase db);

        DataSet GetList(IDataBase db,Pagination page,List<QueryConditionModel> conditions);
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="db"></param>
        /// <param name="menuID">菜单ID</param>
        /// <returns></returns>
        Sys_MenuModel GetDetail(IDataBase db, int menuID);
    }
}