using MyPlatform.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlatform.Model.Enum;

namespace MyPlatform.DBUtility
{
    public class SqlServerDataBase : DBHelperBase, IDataBase
    {
        //TODO:Timeout设置
        //public string ConnectionString;//连接字符串

        #region 构造函数
        /// <summary>
        /// 构造函数，使用Default数据库连接
        /// </summary>
        public SqlServerDataBase()
        {
            DBType = DBEnum.SqlServer;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbname">数据库连接名</param>
        public SqlServerDataBase(string dbCon)
        {
            DBType = DBEnum.SqlServer;
            ConnectionString = GetConStr(dbCon);
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="con"></param>
        public void Open(SqlConnection con)
        {
            if (con == null)
            {
                throw new Exception("数据库连接为空，请确认数据库配置信息！");
            }
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        /// <summary>
        /// 创建SqlCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string sql, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            Open(con);
            return cmd;
        }
        /// <summary>
        /// 创建SqlCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string sql, IDataParameter[] paras, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            foreach (SqlParameter item in paras)
            {
                if ((item.Direction == ParameterDirection.Input || item.Direction == ParameterDirection.InputOutput) && (item.Value == null))
                {
                    item.Value = DBNull.Value;
                }
                cmd.Parameters.Add(item);
            }
            Open(con);
            return cmd;
        }
        /// <summary>
        /// 创建SqlCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string sql, List<SqlParameter> paras, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            foreach (SqlParameter item in paras)
            {
                if ((item.Direction == ParameterDirection.Input || item.Direction == ParameterDirection.InputOutput) && (item.Value == null))
                {
                    item.Value = DBNull.Value;
                }
                cmd.Parameters.Add(item);
            }
            Open(con);
            return cmd;
        }
        /// <summary>
        /// 创建SqlCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string sql, List<IDataParameter> paras, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            foreach (SqlParameter item in paras)
            {
                if ((item.Direction == ParameterDirection.Input || item.Direction == ParameterDirection.InputOutput) && (item.Value == null))
                {
                    item.Value = DBNull.Value;
                }
                cmd.Parameters.Add(item);
            }
            Open(con);
            return cmd;
        }
        #endregion

        #region 执行简单sql方法
        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, con);
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 查询并返回SqlDataReader
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, con);
                    return cmd.ExecuteReader();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行查询语句，并返回第一行第一列数据
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, con);
                    return cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 返回sql查询结果
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>返回DataSet</returns>
        public DataSet Query(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Query(List<string> sqls)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string sql = "";
                    for (int i = 0; i < sqls.Count; i++)
                    {
                        sql += sqls[i] + ";";
                    }
                    SqlCommand cmd = CreateCommand(sql, con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ds;
        }

        #endregion

        #region 执行带参数sql方法
        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, IDataParameter[] paras)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras, con);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public object ExecuteScalar(string sql, IDataParameter[] paras)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras, con);
                    object o = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return o;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行sql并返回查询结果集合
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public DataSet Query(string sql, IDataParameter[] paras)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras, con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// 执行sql并返回查询结果集合
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public DataSet Query(string sql, List<IDataParameter> paras)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = CreateCommand(sql, paras, con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                    cmd.Parameters.Clear();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet Query(List<SqlCommandData> list)
        {
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                Open(con);
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        for (int i = 0; i < list.Count; i++)
                        {
                            cmd.Parameters.Clear();
                            cmd = CreateCommand(list[i].CommandText, list[i].Paras, con);
                            cmd.CommandType = list[i].CommandType;
                            cmd.Transaction = tran;
                            DataTable dt = new DataTable();
                            dt.TableName = "data" + (i == 0 ? "" : i.ToString());
                            switch (list[i].CommandBehavior)
                            {
                                case SqlServerCommandBehavior.ExecuteNonQuery:
                                    int m = cmd.ExecuteNonQuery();
                                    DataColumn dc = new DataColumn("TotalCount");
                                    dt.Columns.Add(dc);
                                    DataRow dr = dt.NewRow();
                                    dr[0] = m;
                                    dt.Rows.Add(dr);
                                    break;
                                case SqlServerCommandBehavior.ExecuteSclar:
                                    DataColumn dcSclar = new DataColumn("TotalCount");
                                    dt.Columns.Add(dcSclar);
                                    DataRow dr3 = dt.NewRow();
                                    dr3[0] = cmd.ExecuteScalar();
                                    dt.Rows.Add(dr3);
                                    break;
                                case SqlServerCommandBehavior.ExecuteReader:
                                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                                    sda.Fill(dt);
                                    //using (SqlDataReader sdr = cmd.ExecuteReader())
                                    //{
                                    //    List<string> names = new List<string>();
                                    //    for (int j = 0; j < sdr.FieldCount; j++)
                                    //    {
                                    //        //names.Add(sdr.GetName(j));
                                    //        DataColumn cn = new DataColumn(sdr.GetName(j));
                                    //        dt.Columns.Add(cn);
                                    //    }
                                    //    while (sdr.Read())
                                    //    {
                                    //        DataRow dr2 = dt.NewRow();
                                    //        for (int k = 0; k < sdr.FieldCount; k++)
                                    //        {
                                    //            dr2[k] = sdr[k];
                                    //        }
                                    //        dt.Rows.Add(dr2);
                                    //    }
                                    //}
                                    break;
                                default:
                                    break;
                            }
                            ds.Tables.Add(dt);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }

            }

            return ds;
        }

        #endregion
        #region 执行存储过程
        public DataSet ExecProcedure(string procedureName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    cmd.Parameters.Clear();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //cmd.ExecuteNonQuery();
            }
            return ds;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        public DataSet ExecProcedure(string procedureName, IDataParameter[] paras)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procedureName, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter para in paras)
                    {
                        if ((para.Direction == ParameterDirection.Input || para.Direction == ParameterDirection.InputOutput) && para.Value == null)
                        {
                            para.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(para);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    cmd.Parameters.Clear();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                //cmd.ExecuteNonQuery();
            }
            return ds;
        }
        #endregion
        #region 执行简单语句事务
        public bool ExecuteTran(List<string> liSql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                bool flag = true;
                con.Open();
                using (SqlTransaction tran = con.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = tran;
                    try
                    {
                        int index = 0;
                        foreach (string sql in liSql)
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            index += 1;
                        }
                        tran.Commit();
                        flag = true;
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        flag = false;
                        throw ex;
                    }
                }
                return flag;
            }
        }
        #endregion
        #region 执行带参数事务
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="tranSqls">事务参数</param>
        /// <returns></returns>
        public bool ExecuteTran(List<SqlCommandData> tranSqls)
        {
            bool flag = true;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlTransaction ts = con.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = ts;
                    try
                    {
                        foreach (SqlCommandData item in tranSqls)
                        {
                            cmd.CommandText = item.CommandText;
                            cmd.CommandType = item.CommandType;
                            if (item.Paras != null)
                            {
                                //SqlCommand参数赋值
                                foreach (SqlParameter para in item.Paras)
                                {
                                    if ((para.Direction == ParameterDirection.Input || para.Direction == ParameterDirection.InputOutput) && para.Value == null)
                                    {
                                        para.Value = DBNull.Value;
                                    }
                                    cmd.Parameters.Add(para);
                                }
                            }
                            if (item.CommandTimeout > 0)
                            {
                                cmd.CommandTimeout = item.CommandTimeout;
                            }
                            switch (item.CommandBehavior)
                            {
                                case SqlServerCommandBehavior.ExecuteNonQuery:
                                    cmd.ExecuteNonQuery();
                                    break;
                                case SqlServerCommandBehavior.ExecuteSclar:
                                    cmd.ExecuteScalar();
                                    break;
                                case SqlServerCommandBehavior.ExecuteReader:
                                    cmd.ExecuteReader();
                                    break;
                                default:
                                    cmd.ExecuteNonQuery();
                                    break;
                            }
                            cmd.Parameters.Clear();
                        }
                        ts.Commit();
                        flag = true;
                    }
                    catch (SqlException ex)
                    {
                        ts.Rollback();
                        flag = false;
                        throw ex;
                    }
                }
                con.Close();
            }
            return flag;
        }
        #endregion
    }
    public enum SqlServerCommandBehavior
    {
        ExecuteNonQuery,
        ExecuteSclar,
        ExecuteReader

    }
}
