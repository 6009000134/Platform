using MyPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class U9TestUtils
    {
        /// <summary>
        /// U9接口Context上下文信息
        /// </summary>
        /// <param name="entCode"></param>
        /// <param name="OrgCode"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        private Dictionary<string, string> GenerateContext(string entCode, string OrgCode, string UserCode)
        {
            Dictionary<string, string> context = new Dictionary<string, string>();
            context.Add("EntCode", entCode);
            context.Add("OrgCode", OrgCode);
            context.Add("UserCode", UserCode);
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EntCode"></param>
        /// <param name="OrgCode"></param>
        /// <param name="UserCode"></param>
        /// <param name="DocName"></param>
        /// <param name="Action"></param>
        /// <param name="inputDataJson">inputData的json字符串</param>
        private Dictionary<string, object> GenerateParam(string EntCode, string OrgCode, string UserCode, string DocName, string Action, string inputDataJson)
        {
            Dictionary<string, object> dicInfo = new Dictionary<string, object>();
            dicInfo.Add("context", GenerateContext(EntCode, OrgCode, UserCode));
            dicInfo.Add("docName", DocName);
            dicInfo.Add("action", Action);
            dicInfo.Add("inputData", Convert.ToBase64String(Encoding.UTF8.GetBytes(inputDataJson)));//InputData需要Base64位编码
            return dicInfo;
        }
        public string CallCustomSV(string EntCode, string OrgCode, string UserCode, string DocName, string Action, string inputDataJson)
        {
            HttpHelper http = new HttpHelper();
            string testUrl = "http://192.168.1.82:90/U9/RestServices/Auctus.CustomSV.ICustomSV.svc/Do";
            Dictionary<string, object> dicInfo = GenerateParam(EntCode, OrgCode, UserCode, DocName, Action, inputDataJson);
            string result = http.Post(testUrl, JSONUtil.GetJson<Dictionary<string, object>>(dicInfo));
            return result;
        }
    }
}
