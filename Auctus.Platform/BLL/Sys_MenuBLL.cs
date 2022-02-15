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
            return dal.Edit(model,db);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnData GetList(Pagination page,List<QueryConditionModel> conditions)
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(defaultCon);
                DataSet ds = dal.GetList(db,page,conditions);
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
                    DataRow[] drs = ds.Tables[0].Select("ParentID=0");
                    if (drs.Length>0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            Sys_MenuModel menu = new Sys_MenuModel();
                            menu.CreatedBy = dr["CreatedBy"].ToString();
                            DataRow[] drs2 = ds.Tables[0].Select("ParentID=1");
                            foreach (DataRow dr2 in drs2)
                            {

                            }
                        }
                    }
                }
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
        //TODO:获取菜单
        public void GetMenuRecursion(DataTable dt,int parentID,List<Sys_MenuModel> li)
        {
            if (dt.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select("ParentID="+parentID.ToString());
                if (drs.Length > 0)
                {
                    foreach (DataRow dr in drs)
                    {
                        Sys_MenuModel menu = new Sys_MenuModel();
                        //menu.ID = dr["ID"].ToString();
                        //menu.CreatedBy = dr["CreatedBy"].ToString();
                        //menu.CreatedDate = dr["CreatedBy"].ToString();
                        //menu.UpdatedBy = dr["CreatedBy"].ToString();
                        //menu.UpdatedDate = dr["CreatedBy"].ToString();
                        //menu.MenuName = dr["CreatedBy"].ToString();
                        //menu.Uri = dr["CreatedBy"].ToString();
                        //menu.ParentID = dr["CreatedBy"].ToString();
                        //menu.vu = dr["CreatedBy"].ToString();                        
                        li.Add(menu);
                        GetMenuRecursion(dt,menu.ID,li);
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
            return dal.GetDetail(db,menuID);
        }

    }
}