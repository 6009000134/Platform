using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// TuShare接口传参
    /// </summary>
    public class APIInputParamModel
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string api_name { get; set; }
        /// <summary>
        /// TuShare的Token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 输入参数：参数名-值
        /// </summary>
        public Dictionary<string,string> @params { get; set; }
        /// <summary>
        /// 输出参数
        /// </summary>
        public string fields { get; set; }
    }
}
