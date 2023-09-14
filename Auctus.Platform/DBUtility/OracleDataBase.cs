using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public class OracleDataBase : DBHelperBase, IDataBase
    {
        public OracleDataBase(string dbCon)
        {
            DBType = DBEnum.Oracle;
            ConnectionString = GetConStr(dbCon);
        }
        public DataSet ExecProcedure(string procedureName)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecProcedure(string procedureName, IDataParameter[] paras)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql, IDataParameter[] paras)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(ConnectionString))
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(paras);
                    //OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    //oda.Fill(ds);
                    con.Open();
                    int i= cmd.ExecuteNonQuery();
                    con.Close();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        public SqlDataReader ExecuteReader(string sql)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql, IDataParameter[] paras)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteTran(List<SqlCommandData> tranSqls)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteTran(List<string> liSql)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleConnection con = new OracleConnection(ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">查询sql</param>
        /// <param name="paras">查询参数</param>
        /// <returns></returns>
        public DataSet Query(string sql, IDataParameter[] paras)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleConnection con = new OracleConnection(ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(paras);
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// 多结果集查询
        /// </summary>
        /// <param name="liSqls">查询sql</param>
        /// <param name="liParas">查询参数</param>
        /// <returns></returns>
        public DataSet Query(List<string> liSqls, List<IDataParameter[]> liParas)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleConnection con = new OracleConnection(ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    for (int i = 0; i < liSqls.Count; i++)
                    {
                        DataSet dsTemp = new DataSet();
                        cmd.CommandText = liSqls[i];
                        cmd.Parameters.AddRange(liParas[i]);
                        OracleDataAdapter oda = new OracleDataAdapter(cmd);
                        oda.Fill(dsTemp);
                        DataTable dt = dsTemp.Tables[0].Copy();
                        ds.Tables.Add(dt);
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// 多结果集查询
        /// </summary>
        /// <param name="liSqls">查询语句集合</param>
        /// <returns></returns>
        public DataSet Query(List<string> liSqls)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleConnection con = new OracleConnection(ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = con;
                    for (int i = 0; i < liSqls.Count; i++)
                    {
                        DataSet dsTemp = new DataSet();
                        cmd.CommandText = liSqls[i];
                        OracleDataAdapter oda = new OracleDataAdapter(cmd);
                        oda.Fill(dsTemp);
                        DataTable dt = dsTemp.Tables[0].Copy();
                        ds.Tables.Add(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Query(string sql, List<IDataParameter> paras)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(List<SqlCommandData> list)
        {
            throw new NotImplementedException();
        }
    }
}
