using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Model;

namespace MyPlatform.SQLServerDAL
{
    //Sys_Menu
    public partial class Sys_MenuDAL : ISys_Menu
    {
        //public MyPlatform.Model.Sys_Menu GetDetailByID(int id)
        //{
        //}
        public Sys_MenuModel GetDetail(IDataBase db, int menuID)
        {
            Sys_MenuModel model = new Sys_MenuModel();
            string sql = @"SELECT a.ID,a.CreatedBy,a.CreatedDate,a.UpdatedBy,a.UpdatedDate,a.MenuName,a.Uri,a.ParentID,b.ID RouterID,b.Path,b.Name,b.Meta,b.Component,b.MenuID
FROM dbo.Sys_Menu a LEFT JOIN dbo.Sys_VueRouter b ON a.ID=b.MenuID
WHERE  a.ID=@MenuID";
            SqlParameter[] pars = { new SqlParameter("@MenuID", menuID) };
            DataSet ds = db.Query(sql, pars);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                model.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                model.CreatedBy = ds.Tables[0].Rows[i]["CreatedBy"].ToString();
                model.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreatedDate"]);
                model.UpdatedBy = ds.Tables[0].Rows[i]["UpdatedBy"].ToString();
                model.UpdatedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["UpdatedDate"]);
                model.MenuName = ds.Tables[0].Rows[i]["MenuName"].ToString();
                model.Uri = ds.Tables[0].Rows[i]["Uri"].ToString();
                model.ParentID = Convert.ToInt32(ds.Tables[0].Rows[i]["ParentID"]);
                //router info
                model.Router.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ParentID"]);
                model.Router.Path = ds.Tables[0].Rows[i]["Path"].ToString();
                model.Router.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                model.Router.Meta = ds.Tables[0].Rows[i]["Meta"].ToString();
                model.Router.Component = ds.Tables[0].Rows[i]["Component"].ToString();
                model.Router.MenuID = Convert.ToInt32(ds.Tables[0].Rows[i]["MenuID"]);
            }
            return model;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Edit(MyPlatform.Model.Sys_MenuModel model, IDataBase db)
        {
            List<SqlCommandData> liSql = new List<SqlCommandData>();
            SqlCommandData scdMenu = new SqlCommandData();
            string sql = @"UPDATE dbo.Sys_Menu SET CreatedBy=@CreatedBy,
CreatedDate=@CreatedDate,
UpdatedBy=@UpdatedBy,
UpdatedDate=@UpdatedDate,
MenuName=@MenuName,
Uri=@Uri,
ParentID=@ParentID
WHERE ID=@ID";
            List<SqlParameter> liMenuParas = new List<SqlParameter> {
                new SqlParameter("@CreatedBy",model.CreatedBy),
                new SqlParameter("@CreatedDate",model.CreatedDate),
                new SqlParameter("@UpdatedBy",model.UpdatedBy),
                new SqlParameter("@UpdatedDate",model.UpdatedDate),
                new SqlParameter("@MenuName",model.MenuName),
                new SqlParameter("@Uri",model.Uri),
                new SqlParameter("@MenuName",model.MenuName),
                new SqlParameter("@ParentID",model.ParentID),
                new SqlParameter("@ID",model.ID)
            };
            scdMenu.CommandBehavior = SqlServerCommandBehavior.ExecuteNonQuery;
            scdMenu.CommandText = sql;
            scdMenu.Paras.AddRange(liMenuParas);

            string sqlRouter = @"	UPDATE dbo.Sys_VueRouter SET Path=@Path,
Name=@Name,
Meta=@Meta,
Component=@Component,
MenuID=@MenuID
WHERE ID=@ID";
            List<SqlParameter> liRouterParas = new List<SqlParameter> {
                new SqlParameter("@Path",model.Router.Path),
                new SqlParameter("@Name",model.Router.Name),
                new SqlParameter("@Meta",model.Router.Meta),
                new SqlParameter("@Component",model.Router.Component),
                new SqlParameter("@MenuID",model.Router.MenuID),
                new SqlParameter("@ID",model.Router.ID),
            };
            SqlCommandData scdRouuter = new SqlCommandData();
            scdRouuter.CommandBehavior = SqlServerCommandBehavior.ExecuteNonQuery;
            scdRouuter.CommandText = sqlRouter;
            scdRouuter.Paras.AddRange(liRouterParas);
            liSql.Add(scdMenu);
            liSql.Add(scdRouuter);
            return db.ExecuteTran(liSql);
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool Add(MyPlatform.Model.Sys_MenuModel model, IDataBase db)
        {
            List<SqlCommandData> liSql = new List<SqlCommandData>();
            SqlCommandData scdMenu = new SqlCommandData();
            string sql = @"INSERT INTO dbo.Sys_Menu
        ( CreatedBy ,
          CreatedDate ,
          UpdatedBy ,
          UpdatedDate ,
          MenuName ,
          Uri ,
          ParentID
        )
VALUES  ( @CreatedBy , -- CreatedBy - nvarchar(20)
          GETDATE() , -- CreatedDate - datetime
          @UpdatedBy , -- UpdatedBy - nvarchar(20)
          GETDATE() , -- UpdatedDate - datetime
          @MenuName , -- MenuName - nvarchar(50)
          @Uri , -- Uri - varchar(300)
          @ParentID -- ParentID - int
        )";
            List<SqlParameter> liMenuParas = new List<SqlParameter> {
                new SqlParameter("@CreatedBy",model.CreatedBy),
                new SqlParameter("@UpdatedBy",model.UpdatedBy),
                new SqlParameter("@MenuName",model.MenuName),
                new SqlParameter("@Uri",model.Uri),
                new SqlParameter("@ParentID",model.ParentID)
            };
            scdMenu.CommandBehavior = SqlServerCommandBehavior.ExecuteNonQuery;
            scdMenu.CommandText = sql;
            scdMenu.Paras.AddRange(liMenuParas);

            string sqlRouter = @"	INSERT INTO dbo.Sys_VueRouter
		        ( Path, Name, Meta, Component, MenuID )
		VALUES  ( @Path, -- Path - varchar(200)
		          @Name, -- Name - varchar(200)
		         @Meta, -- Meta - nvarchar(1000)
		          @Component, -- Component - varchar(500)
		          (select IDENT_CURRENT('sys_menu'))  -- MenuID - int
		          )";
            List<SqlParameter> liRouterParas = new List<SqlParameter> {
                new SqlParameter("@Path",model.Router.Path),
                new SqlParameter("@Name",model.Router.Name),
                new SqlParameter("@Meta",model.Router.Meta),
                new SqlParameter("@Component",model.Router.Component)
            };
            SqlCommandData scdRouuter = new SqlCommandData();
            scdRouuter.CommandBehavior = SqlServerCommandBehavior.ExecuteNonQuery;
            scdRouuter.CommandText = sqlRouter;
            scdRouuter.Paras.AddRange(liRouterParas);
            liSql.Add(scdMenu);
            liSql.Add(scdRouuter);
            return db.ExecuteTran(liSql);
        }
        public DataSet GetMenuTree(IDataBase db)
        {
            string sql = @"
WITH menu(ID,MenuName,ParentID,FullMenuPath) AS
(
SELECT a.ID,a.MenuName,a.ParentID,CAST(a.MenuName  AS VARCHAR(8000))
FROM dbo.Sys_Menu a WHERE a.ParentID=0
UNION ALL 
SELECT b.ID,b.MenuName,b.ParentID,cast (CONVERT(varchar(100),a.FullMenuPath)+'/'+CONVERT(VARCHAR(100),b.MenuName) AS varchar(8000))--,CONVERT(NVARCHAR(MAX),a.FullMenuPath+b.MenuName)
FROM menu a INNER JOIN dbo.Sys_Menu b ON a.ID=b.ParentID
)
SELECT * FROM menu
order by FullMenuPath
";
            return db.Query(sql);
        }
        public DataSet GetList(IDataBase db, Pagination page, List<QueryConditionModel> conditions)
        {
            List<SqlCommandData> liCmd = new List<SqlCommandData>();
            SqlCommandData scd = new SqlCommandData();
            scd.Paras = new List<SqlParameter>();
            scd.CommandBehavior = SqlServerCommandBehavior.ExecuteReader;

            SqlCommandData scdTotal = new SqlCommandData();
            scdTotal.Paras = new List<SqlParameter>();
            scdTotal.CommandBehavior = SqlServerCommandBehavior.ExecuteReader;

            scd.CommandText = @";
WITH menu AS
(
SELECT a.ID,a.CreatedBy,a.CreatedDate,a.UpdatedBy,a.UpdatedDate,a.MenuName,a.Uri,a.ParentID
,b.ID RouterID,b.Path,b.Name,b.Meta,b.Component,b.MenuID
FROM dbo.Sys_Menu a LEFT JOIN dbo.Sys_VueRouter b ON a.ID=b.MenuID
),
Result AS
(
SELECT a.ID,a.CreatedBy,a.CreatedDate,a.UpdatedBy,a.UpdatedDate,a.MenuName,a.Uri,a.ParentID
,a.RouterID,a.Path,a.Name,a.Meta,a.Component,a.MenuID,CAST(a.MenuName  AS VARCHAR(8000))FullPath
FROM menu a WHERE a.ParentID=0
UNION ALL 
SELECT b.ID,b.CreatedBy,b.CreatedDate,b.UpdatedBy,b.UpdatedDate,b.MenuName,b.Uri,b.ParentID
,b.RouterID,b.Path,b.Name,b.Meta,b.Component,b.MenuID,cast (CONVERT(varchar(100),a.FullPath)+'/'+CONVERT(VARCHAR(100),b.MenuName) AS varchar(8000))
FROM Result a INNER JOIN menu b ON a.ID=b.ParentID
)
select * from (SELECT t.*,ROW_NUMBER()OVER(ORDER BY t.FullPath) OrderNo
FROM Result t) t
where 1=1 ";

            scdTotal.CommandText = @"WITH menu AS
(
SELECT a.ID,a.CreatedBy,a.CreatedDate,a.UpdatedBy,a.UpdatedDate,a.MenuName,a.Uri,a.ParentID
,b.ID RouterID,b.Path,b.Name,b.Meta,b.Component,b.MenuID
FROM dbo.Sys_Menu a LEFT JOIN dbo.Sys_VueRouter b ON a.ID=b.MenuID
),
Result AS
(
SELECT a.ID,a.CreatedBy,a.CreatedDate,a.UpdatedBy,a.UpdatedDate,a.MenuName,a.Uri,a.ParentID
,a.RouterID,a.Path,a.Name,a.Meta,a.Component,a.MenuID,CAST(a.MenuName  AS VARCHAR(8000))FullPath
FROM menu a WHERE a.ParentID=0
UNION ALL 
SELECT b.ID,b.CreatedBy,b.CreatedDate,b.UpdatedBy,b.UpdatedDate,b.MenuName,b.Uri,b.ParentID
,b.RouterID,b.Path,b.Name,b.Meta,b.Component,b.MenuID,cast (CONVERT(varchar(100),a.FullPath)+'/'+CONVERT(VARCHAR(100),b.MenuName) AS varchar(8000))
FROM Result a INNER JOIN menu b ON a.ID=b.ParentID
)
SELECT count(1)TotalCount 
FROM Result t
where 1=1 ";
            if (conditions.Count > 0)
            {
                foreach (QueryConditionModel item in conditions)
                {
                    scd.CommandText += item.Key;
                    scdTotal.CommandText += item.Key;
                    switch (item.Operator)
                    {
                        case "=":
                            scd.CommandText += " = ";
                            scdTotal.CommandText += " = ";
                            break;
                        default:
                            scd.CommandText += " " + item.Operator + " ";
                            scdTotal.CommandText += " " + item.Operator + " ";
                            break;
                    }
                    scd.CommandText += "@" + item.Key;
                    scdTotal.CommandText += "@" + item.Key;
                    SqlParameter par = new SqlParameter("@" + item.Key, item.Value);
                    scd.Paras.Add(par);
                    scdTotal.Paras.Add(par);
                }
            }

            SqlParameter parStart = new SqlParameter("@startIndex", page.startIndex);
            SqlParameter parEnd = new SqlParameter("@endIndex", page.endIndex);
            scd.Paras.Add(parStart);
            scd.Paras.Add(parEnd);
            scd.CommandText += " and t.OrderNo>@startIndex and t.OrderNo<@endIndex  ";
            scd.CommandText += " order by t.OrderNo ";
            liCmd.Add(scd);
            liCmd.Add(scdTotal);
            return db.Query(liCmd);
        }
    }
}

