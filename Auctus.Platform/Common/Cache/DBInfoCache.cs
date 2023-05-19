using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyPlatform.Common.Cache
{
    public static class DBInfoCache
    {
        /// <summary>
        /// 获取数据库信息列表
        /// </summary>
        /// <returns></returns>
        public static object GetDBList()
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList");
            if (DBList == null)
            {
                XMLHelper xmlHelper = new XMLHelper();
                //xmlHelper.FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/DataBase.xml");
                xmlHelper.FilePath = "D:\\刘飞\\项目\\lf\\Auctus.Platform\\Auctus.Platform\\ConsoleTest\\DataBase.xml";
                List<Dictionary<string, string>> li = new List<Dictionary<string, string>>();
                XmlNodeList nodeList = xmlHelper.GetNodeList("/root/DB");
                foreach (XmlNode node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        Dictionary<string, string> dicDB = new Dictionary<string, string>();
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            dicDB.Add(item.Name, item.InnerText);
                        }
                        li.Add(dicDB);
                    }
                    else
                    {
                        throw new Exception("数据库信息配置不正确");
                    }
                }
                DBList = li;
                cache.SetCache("Sys_DBList", li);                                
            }
            return DBList;
        }
        /// <summary>
        /// 获取数据库信息列表
        /// </summary>
        /// <returns></returns>
        public static object GetDBListWithoutConStr()
        {
            DataCache cache = new DataCache();
            object DBList = cache.GetCache("Sys_DBList2");
            if (DBList == null)
            {
                XMLHelper xmlHelper = new XMLHelper();
                xmlHelper.FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/DataBase.xml");
                List<Dictionary<string, string>> li = new List<Dictionary<string, string>>();
                XmlNodeList nodeList = xmlHelper.GetNodeList("/root/DB");
                foreach (XmlNode node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        Dictionary<string, string> dicDB = new Dictionary<string, string>();
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            if (item.Name.ToLower()!="connectionstring")
                            {
                                dicDB.Add(item.Name, item.InnerText);
                            }
                        }
                        li.Add(dicDB);
                    }
                    else
                    {
                        throw new Exception("数据库信息配置不正确");
                    }
                }
                DBList = li;
                cache.SetCache("Sys_DBList2", li);
            }
            return DBList;
        }
        /// <summary>
        /// 根据dbCon返回DB所有信息
        /// </summary>
        /// <param name="dbCon"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDBInfo(string dbCon)
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
