using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public interface IDataBase
    {
        DBEnum DBType { get; set; }
        #region 执行简单sql
        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql);
        /// <summary>
        /// 执行查询语句，并返回结果集的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql);
        /// <summary>
        /// 执行查询语句，返回SqlDataReader结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        SqlDataReader ExecuteReader(string sql);
        /// <summary>
        /// 执行查询语句，返回DataSet结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet Query(string sql);
        DataSet Query(List<string> sqls);
        #endregion
        #region 执行带参数sql
        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, IDataParameter[] paras);
        /// <summary>
        /// 执行查询语句，返回第一行第一列查询结果
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        object ExecuteScalar(string sql,IDataParameter[] paras);
        /// <summary>
        /// 执行sql并返回查询结果集
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">参数</param>
        /// <returns></returns>
        DataSet Query(string sql,IDataParameter[] paras);
        DataSet Query(string sql,List<IDataParameter> paras);
        DataSet Query(List<SqlCommandData> list);
        #endregion
        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <returns></returns>
        DataSet ExecProcedure(string procedureName);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns></returns>
        DataSet ExecProcedure(string procedureName, IDataParameter[] paras);

        #endregion
        #region 执行简单sql事务
        bool ExecuteTran(List<string> liSql);

        #endregion
        #region 执行带参数事务
        bool ExecuteTran(List<SqlCommandData> tranSqls);
        #endregion        

    }
}
