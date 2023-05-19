using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyPlatform.Utils;
using MyPlatform.Model;
using MyPlatform.BLL;

namespace MyPlatform.Areas.Web.Controllers
{
    public class TodoController : ApiController
    {
        TodoBLL bll = new TodoBLL();
        [HttpPost]
        public HttpResponseMessage Add([FromBody]Dictionary<string,string> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                 result.S=bll.Add(dic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
        [HttpPost]
        public HttpResponseMessage List()
        {
            ReturnData result = new ReturnData();
            try
            {
                result.D = bll.GetList();
                result.S = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return MyResponseMessage.SuccessJson(result);
        }
    }
}
