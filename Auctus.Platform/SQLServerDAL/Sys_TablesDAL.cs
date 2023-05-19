using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Model;
using System.Data.Common;
using MyPlatform.Common.Cache;

namespace MyPlatform.SQLServerDAL
{
    //Sys_Tables
    public partial class Sys_TablesDAL : ISys_Tables
    {
        string defaultCon = "Default";
        public Sys_TablesDAL()
        {

        }
        #region Extend
        public bool SyncTaleInfo(IDataBase db,Dictionary<string,string> dbInfo, DataSet ds)
        {
            SqlServerDataBase s = (SqlServerDataBase)db;
            string tableName = ds.Tables[0].Rows[0]["name"].ToString();
            string sqlAddTable = string.Format(@"insert into sys_tables(
CreatedBy ,
          CreatedDate ,
          UpdatedBy ,
          UpdatedDate ,
          DBName ,
          DBType ,
          DBTypeCode ,
          TableName ,
          TableName_EN ,
          TableName_CN ,
          Remark ,
          DBCon
)
values(null,getdate(),null,getdate(),'{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}')", dbInfo["DBName"], dbInfo["DBType"], dbInfo["DBTypeCode"],tableName, tableName, tableName, "",dbInfo["DBCon"]);
            List<string> liSqls = new List<string>();
            liSqls.Add(sqlAddTable);
            string sqlAddColumn = "";
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                sqlAddColumn = string.Format(@"INSERT INTO dbo.Sys_Columns
        ( CreatedBy ,
          CreatedDate ,
          UpdatedBy ,
          UpdatedDate ,
          TableID ,
          TableName ,
          ColumnName ,
          ColumnName_EN ,
          ColumnName_CN ,
          ColumnType ,
          Size ,
          IsNullable ,
          DefaultValue ,
          Remark ,
          OrderNo
        )
VALUES  ( N'' ,
          GETDATE() ,
          N'' ,
          GETDATE() ,
          (SELECT IDENT_CURRENT('Sys_Tables')) , 
          '{0}' ,
          '{1}' , -- ColumnName - varchar(50)
          '{2}' , -- ColumnName_EN - varchar(50)
          N'{3}' , -- ColumnName_CN - nvarchar(30)
          '{4}' , -- ColumnType - varchar(50)
          {5}, -- Size - int
          {6} , -- IsNullable - bit
          '' , -- DefaultValue - nvarchar(20)
          N'' , -- Remark - nvarchar(500)
          {7} 
        )",tableName, ds.Tables[1].Rows[i]["Column_Name"].ToString(), ds.Tables[1].Rows[i]["Column_Name"].ToString(), ds.Tables[1].Rows[i]["Column_Name"].ToString(), ds.Tables[1].Rows[i]["Data_Type"].ToString()
        , Convert.ToInt32(ds.Tables[1].Rows[i]["Length"]==DBNull.Value?0:ds.Tables[1].Rows[i]["Length"]), ds.Tables[1].Rows[i]["Is_Nullable"].ToString().ToUpper()=="YES"?1:0, Convert.ToInt32(ds.Tables[1].Rows[i]["Ordinal_Position"])*10);
                liSqls.Add(sqlAddColumn);
            }
            return db.ExecuteTran(liSqls);
        }
        public DataSet GetSysTableByName(IDataBase db, string tableName)
        {

            string sql = string.Format(" select * from sys.objects  a WHERE type = 'U' and name='{0}'", tableName);
            string sql2 = string.Format(" SELECT Table_Name,Column_Name,Ordinal_Position,Is_Nullable,Data_Type,Character_Maximum_Length Length  FROM information_schema.columns where table_name='{0}' order by Ordinal_Position", tableName);
            return db.Query(sql+";"+sql2);
        }
        /// <summary>
        /// 获取数据库sysobjects信息
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public DataSet GetSysTableList(IDataBase db)
        {
            string sql = " select * from sys.objects  a WHERE type = 'U'";
            return db.Query(sql);
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public DataTable GetListByDBName(Dictionary<string, object> dicCondition)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Tables where 1=1 ");
            IDataBase db = DBHelperFactory.Create(defaultCon);
            if (!string.IsNullOrEmpty("DBCon"))
            {
                strSql.Append(" and DBCon=@DBCon");
                SqlParameter[] parameters = { new SqlParameter("@DBCon", SqlDbType.VarChar, 30) };
                parameters[0].Value = "DBCon";
                return db.Query(strSql.ToString(), parameters).Tables[0];
            }
            else
            {
                return db.Query(strSql.ToString()).Tables[0];
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="DBName"></param>
        /// <returns></returns>
        public DataTable GetListByDBName(string DBCon)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from Sys_Tables where 1=1 ");
            IDataBase db = DBHelperFactory.Create(defaultCon);
            if (!string.IsNullOrEmpty(DBCon))
            {
                strSql.Append(" and DBCon=@DBCon");
                SqlParameter[] parameters = { new SqlParameter("@DBCon", SqlDbType.VarChar, 30) };
                parameters[0].Value = DBCon;
                return db.Query(strSql.ToString(), parameters).Tables[0];
            }
            else
            {
                return db.Query(strSql.ToString()).Tables[0];
            }
        }
        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool Exists(string dbName)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 是否存在表,0-不存在，1-存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="dbTypeCode">数据库类型</param>
        /// <returns></returns>
        public ReturnData ExistsTable(IDataBase db, string tableName)
        {
            ReturnData result = new ReturnData();
            try
            {
                string sql = "";
                IDataParameter[] paras = new IDataParameter[1];
                switch (db.DBType)
                {
                    case DBEnum.SqlServer:
                        sql = " SELECT  1 FROM dbo.SysObjects WHERE ID = object_id(@tableName) AND OBJECTPROPERTY(ID, 'IsTable') = 1 ";
                        paras = new IDataParameter[1] { new SqlParameter("@tableName", SqlDbType.VarChar, 30) };
                        paras[0].Value = tableName;
                        result.S = Convert.ToInt32(db.ExecuteScalar(sql, paras)) > 0;
                        //if (!result.S)
                        //{
                        //    result.M = "数据库已经存在表名为：" + tableName + "的表";
                        //}
                        break;
                    case DBEnum.MySql:
                    case DBEnum.Oracle:
                    default:
                        result.SetErrorMsg(db.DBType.ToString() + "数据库未实现！");
                        break;
                }

                result.S = true;

            }
            catch (SqlException ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 统计表数据记录数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbCon">数据库连接名</param>
        /// <returns></returns>
        public int RecordCount(string tableName, string dbCon)
        {
            try
            {
                IDataBase db = DBHelperFactory.Create(dbCon);
                string sql = "";
                sql = "select count(1) from @tableName";
                SqlParameter[] pars = { new SqlParameter("@tableName", SqlDbType.VarChar, 100) };
                pars[0].Value = tableName;
                return Convert.ToInt32(db.ExecuteScalar(sql, pars));
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 创建表以及默认字段CreatedBy、CreatedDate、UpdatedDate、CreatedDate
        /// </summary>
        /// <param name="model">表信息</param>
        /// <returns></returns>
        public ReturnData Add(MyPlatform.Model.Sys_TablesModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                List<SqlCommandData> sqlCommands = new List<SqlCommandData>();//事务参数
                StringBuilder sql = new StringBuilder();
                sql.Append("Create table {0} (");
                sql.Append(" ID int primary key identity(1,1),");
                sql.Append(" CreatedBy nvarchar(30) not null,");
                sql.Append(" CreatedDate DATETIME DEFAULT(GETDATE()) not null,");
                sql.Append(" UpdatedBy nvarchar(30) default(''),");
                sql.Append(" UpdatedDate datetime default(getdate())");
                // sql.Append(" Deleted bit DEFAULT(0)");
                sql.Append(" )");
                SqlCommandData sc = new SqlCommandData();
                sc.CommandText = string.Format(sql.ToString(), model.TableName);
                sqlCommands.Add(sc);
                IDataBase dbCreate = DBHelperFactory.Create(model.DBCon);
                if (dbCreate.ExecuteTran(sqlCommands))
                {

                    if (AddTableInfo(model))//记录表信息和列信息
                    {
                        result.S = true;
                    }
                    else
                    {
                        result.S = false;
                        result.SetErrorMsg("保存表和列信息到系统表失败");
                    }
                }
                else
                {
                    result.S = false;
                    result.SetErrorMsg("表格创建失败！");
                }
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("保存失败：" + ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 将表和信息记录到sys表
        /// </summary>
        /// <param name="model"></param>
        public bool AddTableInfo(MyPlatform.Model.Sys_TablesModel model)
        {
            List<SqlCommandData> sqlCommands = new List<SqlCommandData>();//事务参数
            IDataBase dbDefault = DBHelperFactory.Create(defaultCon);
            SqlCommandData sc2 = SqlFactory.CreateInsertSqlByRef<MyPlatform.Model.Sys_TablesModel>(model);
            sqlCommands.Add(sc2);
            //dbDefault.ExecuteTran(sqlCommands);
            //sqlCommands = new List<SqlCommandData>();
            //SqlCommandData scID = new SqlCommandData();
            //scID.CommandText = "select SCOPE_IDENTITY()";
            //sqlCommands.Add(scID);
            //int id = Convert.ToInt32(dbDefault.ExecuteScalar("select IDENT_CURRENT('Sys_Tables')"));
            SqlCommandData sc3 = new SqlCommandData();
            sc3.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','CreatedBy','CreatedBy','创建人','NVarchar',30,0,'','')";
            sqlCommands.Add(sc3);
            SqlCommandData sc4 = new SqlCommandData();
            sc4.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','CreatedDate','CreatedDate','创建时间','DateTime',0,0,'','')";
            sqlCommands.Add(sc4);
            SqlCommandData sc5 = new SqlCommandData();
            sc5.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','UpdatedBy','UpdatedBy','更新人','NVarchar',30,1,'','')";
            sqlCommands.Add(sc5);
            SqlCommandData sc6 = new SqlCommandData();
            sc6.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
           ([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
           ,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
           ,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
           ,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "',(SELECT IDENT_CURRENT('Sys_Tables')),'"
           + model.TableName + "','UpdatedDate','UpdatedDate','更新时间','DateTime',0,1,'','')";
            sqlCommands.Add(sc6);
            // SqlCommandData sc7 = new SqlCommandData();
            // sc7.CommandText = @"INSERT INTO [dbo].[Sys_Columns]
            //([CreatedBy]           ,[CreatedDate]           ,[UpdatedBy]           ,[UpdatedDate]
            //,[TableID]           ,[TableName]           ,[ColumnName]           ,[ColumnName_EN]
            //,[ColumnName_CN]           ,[ColumnType]           ,[Size]           ,[IsNullable]           ,[DefaultValue]
            //,[Remark])     VALUES ('" + model.CreatedBy + "','" + model.CreatedDate + "','" + model.UpdatedBy + "','" + model.UpdatedDate + "','0',(SELECT IDENT_CURRENT('Sys_Tables')),'"
            //+ model.TableName + "','Deleted','Deleted','是否已删除','Bit',0,1,0,'')";
            // sqlCommands.Add(sc7);
            return dbDefault.ExecuteTran(sqlCommands);
        }
        /// <summary>
        /// 编辑表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(MyPlatform.Model.Sys_TablesModel model)
        {
            //if (RecordCount(model.TableName,model.DBName)>0)
            //{
            //    return false;
            //}
            //当表中无数据时，允许修改表名、列名信息
            string sql = "";
            sql = "UPDATE dbo.Sys_Tables SET TableName_EN=@TableName_EN,TableName_CN=@TableName_CN,Remark=@Remark,UpdatedBy=@UpdatedBy,UpdatedDate=GETDATE() where ID=@ID";
            SqlParameter[] pars = { new SqlParameter("@TableName_EN",SqlDbType.VarChar,50)
            ,new SqlParameter("@TableName_EN",SqlDbType.VarChar,50)
            ,new SqlParameter("@TableName_CN",SqlDbType.VarChar,100)
            ,new SqlParameter("@Remark",SqlDbType.VarChar,100)
            ,new SqlParameter("@UpdatedBy",SqlDbType.VarChar,400)
            ,new SqlParameter("@ID",SqlDbType.Int)
            };
            pars[0].Value = model.TableName_EN;
            pars[1].Value = model.TableName_CN;
            pars[2].Value = model.Remark;
            pars[3].Value = model.UpdatedBy;
            pars[4].Value = model.ID;
            IDataBase db = new SqlServerDataBase();
            return db.ExecuteNonQuery(sql) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ReturnData GetDetail(int tableID, Pagination page)
        {
            ReturnData result = new ReturnData();
            try
            {
                DataSet ds = new DataSet();
                int startIndex = DALUtils.CalStartIndex(page.PageSize, page.PageIndex);
                int endIndex = DALUtils.CalEndIndex(page.PageSize, page.PageIndex);
                string sql = "select * from sys_tables where ID=@ID;select * from (select ROW_NUMBER() OVER(ORDER BY orderNO)RN,* from sys_columns where tableID=@ID)t where t.rn>" + startIndex.ToString() + " and t.rn<" + endIndex.ToString() + ";select count(1)TotalCount from sys_columns where tableID=@ID";
                IDataBase db = DBHelperFactory.Create(defaultCon);
                SqlParameter[] pars = { new SqlParameter("@ID", tableID) };
                ds = db.Query(sql, pars);
                result.D = ds;
                result.S = true;
            }
            catch (Exception ex)
            {
                throw ex;
                //result.SetErrorMsg(ex.Message);
            }

            return result;
        }
        /// <summary>
        /// 刪除表以及相关信息（只是删除表的记录信息，实际表自己手动删除）//TODO:优化-db声明做到BLL中去
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public bool Delete(int tableID)
        {
            IDataBase db = new SqlServerDataBase("Default");
            //无数据的表可以删除            
            string sql = "select ID,DBCon,TableName,DBTypeCode from Sys_tables where id=@tableID";
            SqlParameter[] pars = { new SqlParameter("@tableID", tableID) };
            DataTable dt = db.Query(sql, pars).Tables[0];
            if (dt.Rows.Count > 0)
            {
                IDataBase db2 = DBHelperFactory.Create(dt.Rows[0]["DBCon"].ToString());
                string sql2 = "select count(1) from " + dt.Rows[0]["TableName"].ToString();
                IDataParameter[] pars2 = new IDataParameter[1];
                //TODO:增加多类型数据库操作
                switch (dt.Rows[0]["DBTypeCode"].ToString().ToLower())
                {
                    case "sqlserver":
                        pars2 = new IDataParameter[1] { new SqlParameter("@tableName", dt.Rows[0]["TableName"]) };
                        break;
                    default:
                        break;
                }
                if (Convert.ToInt32(db2.ExecuteScalar(sql2)) == 0)
                {
                    string sqlDelete = "";
                    switch (dt.Rows[0]["DBTypeCode"].ToString().ToLower())
                    {
                        case "sqlserver":
                            sqlDelete = "drop table " + dt.Rows[0]["TableName"].ToString();
                            break;
                        default:
                            break;
                    }
                    db2.ExecuteNonQuery(sqlDelete);
                    IDataParameter[] pars4 = { new SqlParameter("@tableID", tableID) };
                    List<SqlCommandData> li = new List<SqlCommandData>();
                    SqlCommandData scd = new SqlCommandData();
                    scd.CommandText = "delete from  sys_tables  where id=@tableID";
                    scd.Paras = new List<SqlParameter> { new SqlParameter("@tableID", tableID) };
                    li.Add(scd);
                    SqlCommandData scd2 = new SqlCommandData();
                    scd2.CommandText = "delete from   sys_columns  where tableid=@tableID";
                    scd2.Paras = new List<SqlParameter> { new SqlParameter("@tableID", tableID) };
                    li.Add(scd2);
                    return db.ExecuteTran(li);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MyPlatform.Model.Sys_TablesModel GetModel(IDataBase db)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, CreatedBy, CreatedDate, UpdatedBy, UpdatedDate, DBName,DBType,DBTypeCode,DBCon, TableName, TableName_EN, TableName_CN, Remark  ");
            strSql.Append("  from Sys_Tables ");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
            };
            MyPlatform.Model.Sys_TablesModel model = new MyPlatform.Model.Sys_TablesModel();
            DataSet ds = db.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                if (ds.Tables[0].Rows[0]["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedDate"].ToString());
                }
                model.UpdatedBy = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                if (ds.Tables[0].Rows[0]["UpdatedDate"].ToString() != "")
                {
                    model.UpdatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["UpdatedDate"].ToString());
                }
                model.DBName = ds.Tables[0].Rows[0]["TableName"].ToString();
                model.DBType = Convert.ToInt32(ds.Tables[0].Rows[0]["DBType"]);
                model.DBTypeCode = ds.Tables[0].Rows[0]["DBTypeCode"].ToString();
                model.DBCon = ds.Tables[0].Rows[0]["DBCon"].ToString();
                model.TableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                model.TableName_EN = ds.Tables[0].Rows[0]["TableName_EN"].ToString();
                model.TableName_CN = ds.Tables[0].Rows[0]["TableName_CN"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

   

        #endregion
        #region 辅助方法

        #endregion

    }
}

