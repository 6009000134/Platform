using MyPlatform.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class BLLBase
    {   
        public virtual string defaultCon { get; set; }
        public virtual string DalName { get; set; }
        public IDataBase currentDB;
        public string con;
        public BLLBase()
        {
            defaultCon = "Default";
        }
        public void GetDataBase(string con)
        {
            currentDB = DBHelperFactory.Create(con);
        }        
        /// <summary>
        /// 创建BLL层对应调用的DAL对象（截取类名除去BLL的内容，+DAL则为DAL类名）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateInstance<T>()
        {           
            if (string.IsNullOrEmpty(DalName))
            {
                DalName = typeof(T).Name;
                if (!string.IsNullOrEmpty(DalName))
                {
                    DalName = DalName.Substring(1)+"DAL";
                }
            }
            return DALFactory.DataAccess.CreateInstance<T>(DalName);
        }
    }
}
