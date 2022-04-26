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
    public class Sys_UsersBLL: BLLBase
    {
        private readonly ISys_Users dal;
        //private readonly ISys_Users dal = DataAccess.CreateSysUsers();
        public Sys_UsersBLL()
        {
            dal = this.CreateInstance<ISys_Users>();
        }
        public bool Edit(Sys_UsersModel model)
        {
            IDataBase db = DBHelperFactory.Create(defaultCon);
            return dal.Edit(db, model);
        }
        public Sys_UsersModel GetUserByID(int userID)
        {
            IDataBase db = DBHelperFactory.Create(defaultCon);
            return dal.GetUserByID(db, userID);
        }
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
