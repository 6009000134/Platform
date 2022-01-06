using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// 条件
    /// </summary>
    public class QueryConditionModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryConditionModel()
        {
            Operator = "=";
        }
        /// <summary>
        /// 查询字段（sql中请确保此字段唯一，否则请设置TableAlias）
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 查询值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 运算符
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string TableAlias { get; set; }


    }

}
