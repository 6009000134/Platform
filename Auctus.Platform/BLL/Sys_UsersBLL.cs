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
    public class Sys_UsersBLL
    {
        private readonly ISys_Users dal = DataAccess.CreateInstance<ISys_Users>("Sys_UsersDAL");
        //private readonly ISys_Users dal = DataAccess.CreateSysUsers();

        public MyPlatform.Model.Sys_UsersModel GetModelByAccount(string account)
        {            
            return dal.GetModelByAccount(account);
        }
        public bool Exists(MyPlatform.Model.Sys_UsersModel model)
        {
            return dal.Exists(model);
        }
        public bool Exists(string account)
        {
            return dal.Exists(account);
        }
        public int Add(MyPlatform.Model.Sys_UsersModel model)
        {
            return dal.Add(model);
        }

        public DataSet GetList(List<Dictionary<string, string>> condition, Pagination page)
        {
            return dal.GetList(condition,page);
        }
        public void Validate(Model.Sys_UsersModel model)
        {
            if (string.IsNullOrEmpty(model.Account))
            {
                throw new Exception("账号不能为空！");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new Exception("密码不能为空！");                
            }
        }
    }
}
