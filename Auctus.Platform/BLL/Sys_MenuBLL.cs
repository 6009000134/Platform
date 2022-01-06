using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

namespace MyPlatform.BLL {
	 	//Sys_Menu
		public partial class Sys_MenuBLL
	{
		private readonly ISys_Menu dal=DataAccess.CreateInstance<ISys_Menu>("Sys_MenuDAL");
        private string defaultCon = "Default";
		public Sys_MenuBLL()
        { }

        public bool Add(MyPlatform.Model.Sys_MenuModel model)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            return dal.Add(model, db);
        }

        public ReturnData GetMenuTree()
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
                DataSet ds = dal.GetMenuTree(db);
                result.S = true;
                result.D = ds;
            }
            catch (Exception ex)
            {
                throw ex;
                throw;
            }
            return result;
        }
   
	}
}