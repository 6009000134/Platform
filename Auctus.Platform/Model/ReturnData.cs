using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    public class ReturnData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReturnData()
        {
            S = true;
        }
        /// <summary>
        /// 编码
        /// </summary>
        public string C { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string M { get; set; }
        /// <summary>
        /// Success
        /// </summary>
        public bool S { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public object D { get; set; }
        /// <summary>
        /// 设置错误提示,并将返回信息S设置成false
        /// </summary>
        /// <param name="msg"></param>
        public void SetErrorMsg(string msg)
        {
            this.S = false;//失败
            this.M = msg;//错误信息
        }

        /// <summary>
        /// 设置成功消息
        /// </summary>
        /// <param name="msg"></param>
        public void SetSuccessMsg(string msg)
        {
            this.S = true;
            this.M = msg;
        }
    }
}
