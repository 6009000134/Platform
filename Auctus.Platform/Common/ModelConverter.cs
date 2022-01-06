using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyPlatform.Common
{
    public static class ModelConverter<T> where T:new()
    {
        public static IList<T> ConvertToList(DataTable dt)
        {
            // 定义集合
            List<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();

                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列(DataTable列名是不区分大小写的)
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);

                    }
                }

                ts.Add(t);
            }
            return ts;
        }

        public static T ConvertToModelEntity(DataTable dt)
        {

            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            T t = new T();
            foreach (DataRow dr in dt.Rows)
            {
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
            }
            return t;
        }
        public static T FiledIsNullOrEmpty(T t)
        {
            Type tt = t.GetType();
            foreach (PropertyInfo pi in tt.GetProperties())
            {
                if (pi.GetValue(t, null) == null)
                {
                    pi.SetValue(t, "", null);
                }
            }
            return t;
        }
    }
}
