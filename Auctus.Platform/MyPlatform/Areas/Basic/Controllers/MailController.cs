using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    public class MailController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SendMail()
        {
            ReturnData result = new ReturnData();
            MyPlatform.Common.Mail.MailConfig mailConfig = new Common.Mail.MailConfig();
            MyPlatform.Common.Mail.MailHelper mailHelper = new Common.Mail.MailHelper();
            mailHelper.config = mailConfig;
            mailHelper.SendMail();
            result.M = "邮件发送成功！";
            result.S = true;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
    }
}
