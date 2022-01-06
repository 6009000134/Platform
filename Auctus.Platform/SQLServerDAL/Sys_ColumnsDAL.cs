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
    //Sys_Columns
    public partial class Sys_ColumnsDAL : ISys_Columns
    {
        string defaultCon = "Default";
        /// <summary>
        /// 获取列集合
        /// </summary>
        /// <param name="tableID">表ID</param>
        /// <returns></returns>
        public DataSet GetList(int tableID)
        {
            string sql = "SELECT * FROM dbo.Sys_Columns where tableID=@tableID";
            IDataParameter[] pars = { new SqlParameter("tableID", SqlDbType.Int) };
            pars[0].Value = tableID;
            IDataBase db = DBHelperFactory.Create("Default");
            return db.Query(sql, pars);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Add(IDataBase db, Model.Sys_ColumnsModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                // IDataBase db = DBHelperFactory.CreateDBInstance(DBName);
                if (db.DBType == Model.Enum.DBEnum.SqlServer)//SqlServer
                {
                    StringBuilder sb = new StringBuilder();
                    if (model.Size > 0)
                    {
                        sb.Append("alter table " + model.TableName + " add " + model.ColumnName + " " + model.ColumnType + "(" + model.Size + ")");
                    }
                    else
                    {
                        sb.Append("alter table " + model.TableName + " add  " + model.ColumnName + " " + model.ColumnType);
                    }
                    if (model.IsNullable)
                    {
                        sb.Append("  null");
                    }
                    else
                    {
                        sb.Append(" not null ");
                    }
                    //TODO:增加默认值
                    if (db.ExecuteNonQuery(sb.ToString()) > 0)
                    {
                        result.S = true;
                    }
                }
                else if (db.DBType == Model.Enum.DBEnum.MySql)//MySql
                {
                    result.SetErrorMsg("MySql数据库尚未实现！");
                }
                else if (db.DBType == Model.Enum.DBEnum.Oracle)//Oracle
                {
                    result.SetErrorMsg("Oracle数据库尚未实现！");
                }
                else
                {
                    throw new Exception(db.DBType + "数据库类型未知");
                }
                //列信息记录到默认系统库
                IDataBase dbDef = DBHelperFactory.Create(defaultCon);
                string sql = @"INSERT INTO dbo.Sys_Columns
        ( CreatedBy ,
          CreatedDate ,
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
VALUES  (
@CreatedBy ,
          GetDate(),
          @TableID ,
          @TableName ,
          @ColumnName ,
          @ColumnName_EN ,
          @ColumnName_CN ,
          @ColumnType ,
          @Size ,
          @IsNullable ,
          @DefaultValue ,
          @Remark ,
          @OrderNo
        )";
                SqlParameter[] pars = { new SqlParameter("CreatedBy",model.CreatedBy),
                     new SqlParameter("TableID",model.TableID),
                     new SqlParameter("TableName",model.TableName),
                     new SqlParameter("ColumnName",model.ColumnName),
                     new SqlParameter("ColumnName_EN",model.ColumnName_EN),
                     new SqlParameter("ColumnName_CN",model.ColumnName_CN),
                     new SqlParameter("ColumnType",model.ColumnType),
                     new SqlParameter("Size",model.Size),
                     new SqlParameter("IsNullable",model.IsNullable),
                     new SqlParameter("DefaultValue",model.DefaultValue),
                     new SqlParameter("Remark",model.Remark),
                     new SqlParameter("OrderNo",model.OrderNo),

                };
                dbDef.ExecuteNonQuery(sql, pars);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

