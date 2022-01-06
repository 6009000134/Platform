using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace MyPlatform.DBUtility
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class SqlHelper : DBHelperBase
    {
        public DataSet ExecuteSql(string sql)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string strSql, IDataParameter[] pars)
        {
            throw new NotImplementedException();
        }

        public object GetSingle(string strSql, IDataParameter[] pars)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql)
        {
            throw new NotImplementedException();
        }

        public DataSet Query(string sql, IDataParameter[] pars)
        {
            throw new NotImplementedException();
        }
    }
}