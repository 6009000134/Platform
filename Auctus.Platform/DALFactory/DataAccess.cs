using System;
using System.Reflection;
using System.Configuration;
namespace MyPlatform.DALFactory
{
    /// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
    /// </summary>
    public sealed class DataAccess
    {

        private static readonly string defaultAssemblyPath = ConfigurationManager.AppSettings["SQLServerDAL"];
        private static readonly string SqlServerAssemblyPath = ConfigurationManager.AppSettings["SQLServerDAL"];
        private static readonly string OracleAssemblyPath = ConfigurationManager.AppSettings["OracleDAL"];
        private static readonly string MySqlAssemblyPath = ConfigurationManager.AppSettings["MySqlDAL"];
        public DataAccess()
        { }

        #region CreateObject 

        //不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                return objType;
            }
            catch//(System.Exception ex)
            {
                //string str=ex.Message;// 记录错误日志
                return null;
            }

        }
        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch (System.Exception ex)
                {
                    string str = ex.Message;// 记录错误日志                    
                }
            }
            return objType;
        }
        #endregion

        #region 泛型生成
        /// <summary>
        /// 创建数据层接口。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ClassName">类名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string ClassName)
        {

            string ClassNamespace = "";
            string AssemblyPath = defaultAssemblyPath;
            ClassNamespace = "MyPlatform." + AssemblyPath + "." + ClassName;
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (T)objType;
        }
        /// <summary>
        /// 创建数据层接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ClassName">命名空间</param>
        /// <param name="dbType">数据库类型,默认取sqlserver</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string ClassName, string dbType)
        {
            string ClassNamespace = "";
            string AssemblyPath = "";
            switch (dbType.ToLower())
            {
                case "sqlserver":
                    AssemblyPath = SqlServerAssemblyPath;
                    break;
                case "oracle":
                    AssemblyPath = OracleAssemblyPath;
                    break;
                case "mysql":
                    AssemblyPath = MySqlAssemblyPath;
                    break;
                default:
                    AssemblyPath = SqlServerAssemblyPath;
                    break;
            }
            ClassNamespace = "MyPlatform." + AssemblyPath + "." + ClassName;
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (T)objType;
        }
        #endregion
        #region 创建实例
        public static SQLServerDAL.Sys_UsersDAL CreateSysUsers(string className, string DBName)
        {
            return new SQLServerDAL.Sys_UsersDAL();
        }
        #endregion
    }
}