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
    //Sys_Menu
    public partial class Sys_MenuBLL
    {
        private readonly ISys_Menu dal = DataAccess.CreateInstance<ISys_Menu>("Sys_MenuDAL");
        private string defaultCon = "Default";
        public Sys_MenuBLL()
        { }

        public bool Add(MyPlatform.Model.Sys_MenuModel model)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            return dal.Add(model, db);
        }
        public bool Edit(Sys_MenuModel model)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            return dal.Edit(model, db);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnData GetList(Pagination page, List<QueryConditionModel> conditions)
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
                DataSet ds = dal.GetList(db, page, conditions);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnData GetMenuTree()
        {
            ReturnData result = new ReturnData();
            try
            {
                List<Sys_MenuModel> menuLi = new List<Sys_MenuModel>();
                IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
                DataSet ds = dal.GetMenuTree(db);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //GetMenuRecursion(ds.Tables[0], 0, menuLi);
                    DataRow[] drs = ds.Tables[0].Select("ParentID=0");
                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            Sys_MenuModel menu = new Sys_MenuModel();
                            menu.ID = Convert.ToInt32(dr["ID"]);
                            menu.CreatedBy = dr["CreatedBy"].ToString();
                            menu.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            menu.UpdatedBy = dr["UpdatedBy"].ToString();
                            menu.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                            menu.MenuName = dr["MenuName"].ToString();
                            menu.Uri = dr["Uri"].ToString();
                            menu.ParentID = Convert.ToInt32(dr["ParentID"]);
                            menu.Router.ID = Convert.ToInt32(dr["RouterID"]);
                            menu.Router.MenuID = Convert.ToInt32(dr["MenuID"]);
                            menu.Router.Meta = dr["Meta"].ToString();
                            menu.Router.Name = dr["Name"].ToString();
                            menu.Router.Path = dr["Path"].ToString();
                            GetMenuRecursion2(ds.Tables[0], menu);
                            menuLi.Add(menu);
                        }
                    }
                }
                result.S = true;
                result.D = menuLi;
            }
            catch (Exception ex)
            {
                throw ex;
                throw;
            }
            return result;
        }
        public void GetMenuRecursion2(DataTable dt, Sys_MenuModel model)
        {
            if (dt.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select("ParentID=" + model.ID.ToString());
                if (drs.Length > 0)
                {
                    foreach (DataRow dr in drs)
                    {
                        Sys_MenuModel menu = new Sys_MenuModel();
                        menu.ID = Convert.ToInt32(dr["ID"]);
                        menu.CreatedBy = dr["CreatedBy"].ToString();
                        menu.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        menu.UpdatedBy = dr["UpdatedBy"].ToString();
                        menu.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        menu.MenuName = dr["MenuName"].ToString();
                        menu.Uri = dr["Uri"].ToString();
                        menu.ParentID = Convert.ToInt32(dr["ParentID"]);
                        menu.Router.ID = Convert.ToInt32(dr["RouterID"]);
                        menu.Router.MenuID = Convert.ToInt32(dr["MenuID"]);
                        menu.Router.Meta = dr["Meta"].ToString();
                        menu.Router.Name = dr["Name"].ToString();
                        menu.Router.Path = dr["Path"].ToString();
                        model.ChildMenu.Add(menu);
                        GetMenuRecursion2(dt,menu);
                    }
                }
            }
        }
        //TODO:获取菜单
        public void GetMenuRecursion(DataTable dt, int parentID, List<Sys_MenuModel> li)
        {
            if (dt.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select("ParentID=" + parentID.ToString());
                if (drs.Length > 0)
                {
                    foreach (DataRow dr in drs)
                    {
                        Sys_MenuModel menu = new Sys_MenuModel();
                        menu.ID = Convert.ToInt32(dr["ID"]);
                        menu.CreatedBy = dr["CreatedBy"].ToString();
                        menu.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                        menu.UpdatedBy = dr["UpdatedBy"].ToString();
                        menu.UpdatedDate = Convert.ToDateTime(dr["UpdatedDate"]);
                        menu.MenuName = dr["MenuName"].ToString();
                        menu.Uri = dr["Uri"].ToString();
                        menu.ParentID = Convert.ToInt32(dr["ParentID"]);
                        menu.Router.ID = Convert.ToInt32(dr["RouterID"]);
                        menu.Router.MenuID = Convert.ToInt32(dr["MenuID"]);
                        menu.Router.Meta = dr["Meta"].ToString();
                        menu.Router.Name = dr["Name"].ToString();
                        menu.Router.Path = dr["Path"].ToString();
                        li.Add(menu);
                        GetMenuRecursion(dt, menu.ID, li);
                    }
                }
            }
        }
        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        /// <returns></returns>
        public Sys_MenuModel GetDetailByID(int menuID)
        {
            IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
            return dal.GetDetail(db, menuID);
        }

    }
}