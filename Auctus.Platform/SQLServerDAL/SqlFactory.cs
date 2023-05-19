using MyPlatform.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.SQLServerDAL
{
    public class SqlFactory
    {
        public static SqlCommand CreateInsertSqlByRef<T>(T t,SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection=con;
            PropertyInfo[] pis = t.GetType().GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert Into " + t.GetType().Name);
            sql.Append(" (");
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    sql.Append(pi.Name + ",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            sql.Append(" values (");
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    sql.Append(" @" + pi.Name + ",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            SqlParameter[] paras = new SqlParameter[pis.Length];
            int index = 0;
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    SqlParameter par = new SqlParameter("@ID", pi.GetValue(t));
                    paras[index] = par;
                    index += 1;
                }
            }
            cmd.CommandText = sql.ToString();
            cmd.Parameters.AddRange(paras);
            return cmd;
        }
        /// <summary>
        /// 创建SqlCommandData
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public static SqlCommandData CreateInsertSqlByRef<T>(T t)
        {
            SqlCommandData scd = new SqlCommandData();

            PropertyInfo[] pis = t.GetType().GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert Into " + t.GetType().Name);
            sql.Append(" (");
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    sql.Append(pi.Name + ",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            sql.Append(" values (");
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    sql.Append(" @" + pi.Name + ",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            List<SqlParameter> paras = new List<SqlParameter>();
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    object o=pi.GetValue(t);
                    SqlParameter par = new SqlParameter("@"+pi.Name, pi.GetValue(t));
                    paras.Add(par);
                }
            }
            scd.CommandText = sql.ToString();
            scd.Paras = paras;
            return scd;
        }
        /// <summary>
        /// 创建SqlCommandData
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        public static SqlCommandData CreateInsertSqlByRefTable<T>(T t,string tableName)
        {
            SqlCommandData scd = new SqlCommandData();

            PropertyInfo[] pis = t.GetType().GetProperties();
            StringBuilder sql = new StringBuilder();
            sql.Append("Insert Into " + tableName);
            sql.Append(" (");
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    sql.Append(pi.Name + ",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            sql.Append(" values (");
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    sql.Append(" @" + pi.Name + ",");
                }
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            List<SqlParameter> paras = new List<SqlParameter>();
            foreach (PropertyInfo pi in pis)
            {
                if (pi.Name != "ID")
                {
                    object o = pi.GetValue(t);
                    SqlParameter par = new SqlParameter("@" + pi.Name, pi.GetValue(t));
                    paras.Add(par);
                }
            }
            scd.CommandText = sql.ToString();
            scd.Paras = paras;
            return scd;
        }
    }
}
