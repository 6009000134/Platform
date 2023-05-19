using System;
using System.Data;
using MyPlatform.Model;
using System.Collections.Generic;
using MyPlatform.DBUtility;

namespace MyPlatform.IDAL
{
    /// <summary>
    /// 接口层Sys_Users
    /// </summary>
    public interface ISys_Users
    {
        #region Extend by liufei
        /// <summary>
        /// 验证账号是否注册
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        bool Exists(string Account);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Exists(Sys_UsersModel model);
        /// <summary>
        /// 通过账号获取账号信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        Sys_UsersModel GetModelByAccount(string Account);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(MyPlatform.Model.Sys_UsersModel model);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        DataSet GetList(List<Dictionary<string, string>> condition, Pagination page);
        /// <summary>
        /// 根据ID获取用户信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Sys_UsersModel GetUserByID(IDataBase db,int userID);
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Edit(IDataBase db, Sys_UsersModel model);
        #endregion
    }
}