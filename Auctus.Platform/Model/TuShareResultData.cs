using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// 返回数据集合
    /// </summary>
    public class TuShareResultData
    {
        /// <summary>
        /// 返回字段
        /// </summary>
        public List<string> fields { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<List<string>> items { get; set; }
        /// <summary>
        /// has_more
        /// </summary>
        public bool has_more { get; set; }

    }
}
