using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Common;
using MyPlatform.Model;

namespace MyPlatform.SQLServerDAL
{
    //Sys_Users
    public partial class Sys_UsersDAL : ISys_Users
    {
        string dbCon = "Default";

        public DataSet GetList(List<Dictionary<string, string>> condition, Pagination page)
        {
            DataSet ds = new DataSet();
            int beginIndex = DALUtils.CalStartIndex(page);
            int endIndex = DALUtils.CalEndIndex(page);
            List<SqlParameter> pars = new List<SqlParameter>();
            string sql = @"SELECT *FROM (
SELECT *,ROW_NUMBER()OVER(ORDER BY a.CreatedDate DESC)RN
FROM dbo.Sys_Users a Where 1=1 ";
            string sqlCondition = "";
            foreach (Dictionary<string, string> item in condition)
            {
                if (item.ContainsKey("UserName"))
                {
                    sqlCondition += " AND UserName=@UserName";
                    SqlParameter par = new SqlParameter("@UserName", item["UserName"]);
                    pars.Add(par);
                }
                if (item.ContainsKey("Account"))
                {
                    sqlCondition += " AND Account=@Account";
                    SqlParameter par = new SqlParameter("@Account", item["Account"]);
                    pars.Add(par);
                }
            }
            
            sql += sqlCondition+" ) t WHERE t.RN>" + beginIndex.ToString() + " AND t.RN<" + endIndex.ToString();

            string sqlCount = @"SELECT Count(1)TotalCount 
FROM dbo.Sys_Users a Where 1=1 "+sqlCondition;
            
            IDataBase db = new SqlServerDataBase(dbCon);
            SqlCommandData scd = new SqlCommandData();
            scd.CommandBehavior = SqlServerCommandBehavior.ExecuteReader;
            scd.CommandText = sql;
            scd.Paras = pars;
            SqlCommandData scd2 = new SqlCommandData();
            scd2.CommandBehavior = SqlServerCommandBehavior.ExecuteSclar;
            scd2.CommandText = sqlCount;
            scd2.Paras = pars;
            //scd.CommandBehavior = SqlServerCommandBehavior.ExecuteSclar;
            List<SqlCommandData> liParam = new List<SqlCommandData>();
            liParam.Add(scd);
            liParam.Add(scd2);
            ds = db.Query(liParam);
            //ds.Tables[0].TableName = "data";
            return ds;
        }
        public MyPlatform.Model.Sys_UsersModel GetModelByAccount(string account)
        {
            try
            {
                IDataBase db = new SqlServerDataBase(dbCon);
                MyPlatform.Model.Sys_UsersModel user = new Model.Sys_UsersModel();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" select * from Sys_Users where deleted=0 and Account=@Account");
                SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30) };
                parameters[0].Value = account;
                DataTable dt = db.Query(strSql.ToString(), parameters).Tables[0];
                //DbHelperSQL.Query(strSql, parameters);
                user = ModelConverter<MyPlatform.Model.Sys_UsersModel>.ConvertToModelEntity(dt);
                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 登录验证账号密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists(MyPlatform.Model.Sys_UsersModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from sys_users where deleted=0 and Account=@Account and password=@Password");
            SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30), new SqlParameter("@Password", SqlDbType.VarChar, 30) };
            parameters[0].Value = model.Account;
            parameters[1].Value = model.Password;
            IDataBase db = new SqlServerDataBase(dbCon);
            return Convert.ToInt32(db.ExecuteScalar(strSql.ToString(), parameters)) == 0 ? false : true;
        }
        /// <summary>
        /// 检测账号是否存在
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public bool Exists(string Account)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sys_Users");
            strSql.Append(" where ");
            strSql.Append(" deleted=0 and Account=@Account ");
            SqlParameter[] parameters = { new SqlParameter("@Account", SqlDbType.VarChar, 30) };
            parameters[0].Value = Account;
            IDataBase db = new SqlServerDataBase(dbCon);
            return Convert.ToInt32(db.ExecuteScalar(strSql.ToString(), parameters)) == 0 ? false : true;

        }

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sys_Users");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;
            IDataBase db = new SqlServerDataBase(dbCon);
            return Convert.ToInt32(db.ExecuteScalar(strSql.ToString(), parameters)) == 0 ? false : true;
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MyPlatform.Model.Sys_UsersModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Sys_Users(");
            strSql.Append("CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Deleted,Account,Password,UserName");
            strSql.Append(") values (");
            strSql.Append("@CreatedBy,@CreatedDate,@UpdatedBy,@UpdatedDate,@Deleted,@Account,@Password,@UserName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreatedBy", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@CreatedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@UpdatedBy", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@UpdatedDate", SqlDbType.DateTime) ,
                        new SqlParameter("@Deleted", SqlDbType.Int,4) ,
                        new SqlParameter("@Account", SqlDbType.VarChar,18) ,
                        new SqlParameter("@Password", SqlDbType.VarChar,20) ,
                        new SqlParameter("@UserName", SqlDbType.NVarChar,20)

            };

            parameters[0].Value = model.CreatedBy;
            parameters[1].Value = model.CreatedDate;
            parameters[2].Value = model.UpdatedBy;
            parameters[3].Value = model.UpdatedDate;
            parameters[4].Value = model.Deleted;
            parameters[5].Value = model.Account;
            parameters[6].Value = model.Password;
            parameters[7].Value = model.UserName;

            IDataBase db = new SqlServerDataBase(dbCon);
            return db.ExecuteNonQuery(strSql.ToString(), parameters);
        }
    }
}

