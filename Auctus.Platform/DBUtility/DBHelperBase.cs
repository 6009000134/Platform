using MyPlatform.Common.Cache;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public class DBHelperBase
    {
        public string ConnectionString;
        public DBEnum DBType { get; set; }

        public DBHelperBase()
        {
      
        }
        //public DBHelperBase(string dbCon)
        //{
        //    ConnectionString = GetDBConnection(dbCon);
        //}
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        public string GetConStr(string dbCon)
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            if (DBList == null)
            {
                DBList = DBInfoCache.GetDBList();
            }
            List<Dictionary<string, string>> li = DBList as List<Dictionary<string, string>>;
            Dictionary<string, string> s = new Dictionary<string, string>();
            foreach (Dictionary<string, string> dic in li)
            {
                if (dic["DBCon"].ToString().ToLower() == dbCon.ToLower())
                {
                    return dic["ConnectionString"].ToString().ToLower();                    
                }
            }
            return "";
        }
        /// <summary>
        /// 根据dbCon获取数据库类型
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        public string GetDBType(string dbCon)
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            if (DBList == null)
            {
                DBList = DBInfoCache.GetDBList();
            }
            List<Dictionary<string, string>> li = DBList as List<Dictionary<string, string>>;
            Dictionary<string, string> s = new Dictionary<string, string>();
            foreach (Dictionary<string, string> dic in li)
            {
                if (dic["DBCon"].ToString().ToLower() == dbCon.ToLower())
                {
                    return dic["DBTypeCode"].ToString().ToLower();
                }
            }
            return "";
        }
        /// <summary>
        /// 根据dbCon返回DB所有信息
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDBInfo(string dbCon)
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            if (DBList == null)
            {
                DBList = DBInfoCache.GetDBList();
            }
            List<Dictionary<string, string>> li = DBList as List<Dictionary<string, string>>;
            Dictionary<string, string> s = new Dictionary<string, string>();
            foreach (Dictionary<string, string> dic in li)
            {
                if (dic["DBCon"].ToString().ToLower() == dbCon.ToLower())
                {
                    return dic;
                }
            }
            return null;

        }

    }
}
