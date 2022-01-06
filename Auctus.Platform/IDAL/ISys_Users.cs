using System;
using System.Data;
using MyPlatform.Model;
using System.Collections.Generic;

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
        DataSet GetList(List<Dictionary<string, string>> condition, Pagination page);
        #endregion
    }
}