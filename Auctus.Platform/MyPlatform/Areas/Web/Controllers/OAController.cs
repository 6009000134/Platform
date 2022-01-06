using MyPlatform.Model;
using MyPlatform.Model.Chart;
using MyPlatform.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// OA数据获取
    /// </summary>
    [AllowAnonymous]
    public class OAController : ApiController
    {
        BLL.BLL4OAChart chartBLL = new BLL.BLL4OAChart();

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage GetData(JObject obj)
        {
            ReturnData result = new ReturnData();
            string strTypes = "";
            string xmbm = "";
            if (obj.ContainsKey("types"))
            {
                strTypes = obj["types"].ToString();
            }
            if (obj.ContainsKey("xmbm"))
            {
                xmbm = obj["xmbm"].ToString();
            }
            try
            {
                result = chartBLL.GetData(xmbm,strTypes);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }


    }
}
