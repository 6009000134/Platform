using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// TuShare接口返回结果
    /// </summary>
    public class TuShareResult
    {
        /// <summary>
        /// request_id
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public TuShareResultData data { get; set; }
    }
   
}
