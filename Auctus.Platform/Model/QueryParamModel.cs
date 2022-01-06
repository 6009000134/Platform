using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// 查询条件对象
    /// </summary>
    public class QueryParamModel
    {
        /// <summary>
        /// 查询字段名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 查询字段内容
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 查询方式
        /// </summary>
        public string type { get; set; } = "1";
    }
    /// <summary>
    /// 查询类型 
    /// </summary>
    public enum QueryType
    {
        Equals = 0,
        Like = 1
    }
}
