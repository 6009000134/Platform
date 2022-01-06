using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// 查询页面
    /// </summary>
    public class QueryViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 类型（表/视图/存储过程）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 名称（表名/视图名/存储过程名）
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 查询视图详细信息（列名）
    /// </summary>
    public class QueryViewDetail
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime UpdatedDate { get; set; }
        /// <summary>
        /// ViewID
        /// </summary>
        public string ViewID { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 数据库链接
        /// </summary>
        public string DBCon { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public int Precision { get; set; }
    }
}
