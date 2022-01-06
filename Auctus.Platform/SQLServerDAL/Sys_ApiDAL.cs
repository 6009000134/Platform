using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPlatform.IDAL;
using System.Threading.Tasks;
using MyPlatform.Model;
using System.Data;
using MyPlatform.DBUtility;
using System.Data.SqlClient;
using MyPlatform.Common;

namespace MyPlatform.SQLServerDAL
{
    public class Sys_ApiDAL : ISys_Api
    {
        string defaultCon = "Default";
        /// <summary>
        /// 获取以ApiName为表名的表数据
        /// </summary>
        /// <param name="db"></param>
        /// <param name="apiID"></param>
        /// <returns></returns>
        public DataSet GetDataByApiName(IDataBase db, string apiName)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = string.Format("select * from {0}", apiName);
                ds = db.Query(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
     
            return ds;
        }

        /// <summary>
        /// 获取股票代码/列表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tsCode"></param>
        /// <returns></returns>
        public DataSet GetTsCode(IDataBase db, string tsCode)
        {
            DataSet ds = new DataSet();
            string sql = "";
            SqlParameter[] pars;
            if (string.IsNullOrEmpty(tsCode))
            {
                sql = "select * from stock_basic ";
                ds = db.Query(sql);
            }
            else
            {
                sql = "select * from stock_basic where patindex('@pat',ts_code)>0";
                pars = new SqlParameter[1] { new SqlParameter("@pat", "%" + tsCode + "%") };
                ds = db.Query(sql, pars);
            }
            return ds;
        }
        public Model.Sys_APIModel GetApiInfo(IDataBase db, int apiID)
        {
            MyPlatform.Model.Sys_APIModel api = new Sys_APIModel();
            try
            {
                string sqlSelect = "select * from sys_Apis where ID=@ID";
                SqlParameter[] pars = { new SqlParameter("@ID", apiID) };
                DataSet ds = db.Query(sqlSelect, pars);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    api.ApiName = ds.Tables[0].Rows[0]["ApiName"].ToString();
                    api.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                    api.CreatedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedDate"]);
                    api.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    api.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                    api.UpdatedBy = ds.Tables[0].Rows[0]["UpdatedBy"].ToString();
                    api.UpdatedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["UpdatedDate"]);
                }
                else
                {
                    throw new Exception("API信息找不到");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return api;
        }
        public ReturnData GetApiResult(IDataBase db, TuShareResult data, int apiID)
        {
            ReturnData result = new ReturnData();
            try
            {
                string sqlSelect = "select * from sys_Apis where ID=@ID;select * from api_output where apiID=@ID";
                SqlParameter[] pars = { new SqlParameter("@ID", apiID) };
                DataSet ds = db.Query(sqlSelect, pars);
                string tableName = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tableName = ds.Tables[0].Rows[0]["ApiName"].ToString();
                }
                else
                {
                    throw new Exception("不存在ID为" + apiID.ToString() + "的API信息");
                }
                //TODO:检测表是否存在，不存在则调用CreateApiTable创建
                List<string> liInserts = new List<string>();
                string sql = "";
                StringBuilder sqlSb = new StringBuilder();
                if (data.data.items.Count > 0)
                {
                    foreach (List<string> item in data.data.items)
                    {
                        sqlSb.Append("insert into " + tableName);
                        sqlSb.Append("(");
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            sqlSb.Append("[" + dr["ParamName"].ToString() + "],");
                        }
                        sqlSb.Remove(sqlSb.Length - 1, 1);
                        sqlSb.Append(")");
                        sqlSb.Append(" values (");
                        for (int i = 0; i < item.Count; i++)
                        {
                            if (item[i] == null)
                            {
                                sqlSb.Append("'',");
                            }
                            else
                            {
                                sqlSb.Append("'" + item[i].Replace("'", "''") + "',");
                            }
                        }

                        sqlSb.Remove(sqlSb.Length - 1, 1);
                        sqlSb.Append(");");
                        //sql += "insert into " + tableName;
                        //sql += "(";
                        //foreach (DataRow dr in ds.Tables[1].Rows)
                        //{
                        //    sql += dr["ParamName"].ToString() + ",";
                        //}
                        //sql = sql.TrimEnd(',');
                        //sql += ")";
                        //sql += " values (";
                        //for (int i = 0; i < item.Count; i++)
                        //{
                        //    if (item[i] == null)
                        //    {
                        //        sql += "'',";
                        //    }
                        //    else
                        //    {
                        //        sql += "'" + item[i].Replace("'", "''") + "',";
                        //    }
                        //}

                        //sql = sql.TrimEnd(',');
                        //sql += ");";
                    }
                }
                liInserts.Add(sqlSb.ToString());
                if (db.ExecuteTran(liInserts))
                {
                    result.S = true;
                }
                else
                {
                    result.S = false;
                    result.M = "插入数据失败";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ReturnData CreateApiTable(IDataBase db, int apiID)
        {
            ReturnData result = new ReturnData();
            try
            {
                string sql = "select * from sys_Apis where ID=@ID;select * from api_output where apiID=@ID";
                SqlParameter[] pars = { new SqlParameter("@ID", apiID) };
                string tableName = "";
                DataSet ds = db.Query(sql, pars);
                List<string> liSql = new List<string>();
                string sqlTable = "create table [";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tableName = ds.Tables[0].Rows[0]["ApiName"].ToString();
                    sqlTable += tableName + "]";
                    sqlTable += "(";
                }
                else
                {
                    throw new Exception("不存在ID为" + apiID.ToString() + "的API信息");
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        sqlTable += "[" + item["ParamName"].ToString() + "]";
                        switch (item["ParamType"].ToString().ToLower())
                        {
                            case "str":
                                sqlTable += " varchar(50) ,";
                                break;
                            case "float":
                                sqlTable += " float ,";
                                break;
                            case "int":
                                sqlTable += " int ,";
                                break;
                            default:
                                break;
                        }
                        string sqlExtend = "execute sp_addextendedproperty 'MS_Description','" + item["Description"].ToString() + "','user','dbo','table','" + tableName + "','column','" + item["ParamName"] + "';";
                        liSql.Add(sqlExtend);
                    }
                }
                else
                {
                    result.SetErrorMsg("不存在ID为" + apiID.ToString() + "的API信息");
                }
                sqlTable = sqlTable.Trim(',') + ")";
                liSql.Insert(0, sqlTable);
                if (db.ExecuteTran(liSql))
                {
                    result.S = true;
                }
                else
                {
                    result.SetErrorMsg("创建失败，请联系管理员！");
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return result;
        }

        public DataSet GetDetail(int apiID)
        {
            DataSet ds = new DataSet();
            string sql = "select * from sys_apis where ID=@ID;select * from api_input where apiid=@ID;select * from api_output where apiid=@ID;";
            SqlParameter[] pars = { new SqlParameter("@ID", apiID) };
            IDataBase db = DBHelperFactory.Create(defaultCon);
            ds = db.Query(sql, pars);
            return ds;
        }
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Add(Dictionary<object, object> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                IDataBase db = DBHelperFactory.Create(defaultCon);
                //保存API信息
                string sqlApi = @"insert into sys_apis(Title ,
          ApiName ,
          Description ,
          CreatedBy ,
          CreatedDate           
          ) values(@title,@apiName,@description,'',getdate())";
                List<SqlCommandData> liSql = new List<SqlCommandData>();
                SqlCommandData cmdApi = new SqlCommandData();
                cmdApi.CommandText = sqlApi;
                cmdApi.Paras = new List<SqlParameter>{
                    new SqlParameter("@title", dic["title"]),
                new SqlParameter("@apiName", dic["apiName"]),
                new SqlParameter("@description", dic["description"])
                };
                liSql.Add(cmdApi);
                //保存输入参数信息
                string sqlInput = "";
                string[] arrInput = (string[])dic["inputParam"];
                List<SqlParameter> parInput = new List<SqlParameter>();
                for (int i = 0; i < arrInput.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        sqlInput = @"insert into api_input( ApiID ,
          ParamName ,
          ParamType ,
          IsRequired ,
          Description ,
          OrderNo ,
          CreatedBy ,
          CreatedDate) values((SELECT IDENT_CURRENT('Sys_Apis')),@input" + i.ToString();
                        SqlParameter param = new SqlParameter("@input" + i.ToString(), arrInput[i]);
                        parInput[i % 4] = param;
                    }
                    else if (i % 4 == 3)
                    {
                        sqlInput += ",@input" + i.ToString() + "," + (i * 40).ToString() + ",'',getdate())";
                        SqlParameter param = new SqlParameter("@input" + i.ToString(), arrInput[i]);
                        parInput.Add(param);
                        SqlCommandData cmdInput = new SqlCommandData();
                        cmdInput.CommandText = sqlInput;
                        cmdInput.Paras = parInput;
                        liSql.Add(cmdInput);
                        //重新声明新的输入参数信息
                        parInput = new List<SqlParameter>() ;
                        sqlInput = "";
                    }
                    else
                    {
                        sqlInput += ",@input" + i.ToString();
                        SqlParameter param = new SqlParameter("@input" + i.ToString(), arrInput[i]);
                        parInput[i % 4] = param;
                    }
                }
                //保存输出参数信息
                string sqlOutput = "";
                string[] arrOutput = (string[])dic["outputParam"];
                int outputColumns = Convert.ToInt32(dic["outputColumns"]);
                List<SqlParameter> parOutput = new List<SqlParameter>();
                if (outputColumns == 4)
                {
                    for (int i = 0; i < arrOutput.Length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            sqlOutput = @"insert into api_output( ApiID ,
          ParamName ,
          ParamType ,
          IsRequired ,
          Description ,
          OrderNo ,
          CreatedBy ,
          CreatedDate) values((SELECT IDENT_CURRENT('Sys_Apis')),@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 4] = param;
                        }
                        else if (i % 4 == 3)
                        {
                            sqlOutput += ",@output" + i.ToString() + "," + (i * 40).ToString() + ",'',getdate())";
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput.Add(param);
                            SqlCommandData cmdOutput = new SqlCommandData();
                            cmdOutput.CommandText = sqlOutput;
                            cmdOutput.Paras = parOutput;
                            liSql.Add(cmdOutput);
                            //重新声明新的输入参数信息
                            parOutput = new List<SqlParameter>();
                            sqlOutput = "";
                        }
                        else
                        {
                            sqlOutput += ",@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 4] = param;
                        }
                    }
                }
                else if (outputColumns == 3)
                {
                    for (int i = 0; i < arrOutput.Length; i++)
                    {
                        if (i % 3 == 0)
                        {
                            sqlOutput = @"insert into api_output( ApiID ,
          ParamName ,
          ParamType ,
          IsRequired ,
          Description ,
          OrderNo ,
          CreatedBy ,
          CreatedDate)  values((SELECT IDENT_CURRENT('Sys_Apis')),@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 3] = param;
                        }
                        else if (i % 3 == 2)
                        {
                            sqlOutput += ",'',@output" + i.ToString() + "," + (i * 40).ToString() + ",'',getdate())";
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 3] = param;
                            SqlCommandData cmdOutput = new SqlCommandData();
                            cmdOutput.CommandText = sqlOutput;
                            cmdOutput.Paras = parOutput;
                            liSql.Add(cmdOutput);
                            //重新声明新的输入参数信息
                            parOutput = new List<SqlParameter>();
                            sqlOutput = "";
                        }
                        else
                        {
                            sqlOutput += ",@output" + i.ToString();
                            SqlParameter param = new SqlParameter("@output" + i.ToString(), arrOutput[i]);
                            parOutput[i % 3] = param;
                        }
                    }
                }
                if (db.ExecuteTran(liSql))
                {
                    result.S = true;
                }
                else
                {
                    result.SetErrorMsg("添加失败！");
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// 删除API
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        public ReturnData Delete(int apiID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 修改API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnData Edit(Sys_APIModel model)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 查询API信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataSet GetList(string condition)
        {
            DataSet ds = new DataSet();
            condition = "%" + condition + "%";
            string sql = "select * from Sys_APIs WHERE PATINDEX(@condition,Title+ApiName+Description)>0";
            IDataBase db = DBHelperFactory.Create(defaultCon);
            IDataParameter[] pars = { new SqlParameter("@condition", condition) };
            return db.Query(sql, pars);

        }
        /// <summary>
        /// 获取未同步每日指标的日期
        /// </summary>
        /// <param name="db"></param>
        /// <param name="exchange">交易所代码</param>
        /// <returns></returns>
        public DataTable GetNoDataCalendar(IDataBase db,string exchange)
        {
            string sql =string.Format(@"SELECT a.* FROM dbo.trade_cal a LEFT JOIN dbo.daily_basic b ON a.cal_date=b.trade_date 
WHERE b.trade_date IS NULL and a.is_open='1' and a.exchange='{0}'",exchange);
            return db.Query(sql).Tables[0];
        }
    }
}
