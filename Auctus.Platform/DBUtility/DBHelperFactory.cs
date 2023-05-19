using MyPlatform.Common.Cache;
using MyPlatform.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public static class DBHelperFactory
    {
        /// <summary>
        /// 根据数据库连接名，返回对应DBHelper实例
        /// </summary>
        /// <param name="conName"></param>
        /// <returns></returns>
        public static IDataBase Create(string conName)
        {
            IDataBase db;
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            string dbType = "";
            if (DBList == null)
            {
                DBList = DBInfoCache.GetDBList();
            }
            List<Dictionary<string, string>> li = DBList as List<Dictionary<string, string>>;
            foreach (Dictionary<string, string> dic in li)
            {
                if (dic["DBCon"].ToString().ToLower() == conName.ToLower())
                {
                    dbType = dic["DBTypeCode"].ToString().ToLower();
                    break;
                }
            }
            if (string.IsNullOrEmpty(dbType))
            {
                throw new Exception("不能打开数据，数据库连接为空！");
            }
            switch (dbType.ToLower())
            {
                case "sqlserver":
                    db = new SqlServerDataBase(conName);
                    break;
                case "oracle":
                    db = new OracleDataBase(conName);
                    break;
                case "mysql":
                    throw new Exception("系统暂不支持MySql数据库");
                    break;
                default:
                    db = new SqlServerDataBase();
                    break;
            }
            return db;
        }
    }
}
