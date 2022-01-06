using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyPlatform.Common.Mail
{
    public class MailHelper
    {
        // 邮件配置和信息
        public MailConfig config { get; set; }
        /// <summary>
        /// 发送邮件
        /// </summary>
        public void SendMail()
        {
            try
            {
                MailMessage mail = GetMail();
                SmtpClient client = new SmtpClient(config.Host, config.Port);//TODO:配置在web.config中
                client.Credentials = new NetworkCredential(config.Email, config.Password);
                client.Send(mail);
            }
            catch (Exception ex)
            {           
                throw ex;
            }
        }
        /// <summary>
        /// 设置邮件信息
        /// </summary>
        /// <returns></returns>
        private MailMessage GetMail()
        {
            MailMessage mailMsg = new MailMessage();
            // Attachment a = new Attachment("sss");                        
            if (!string.IsNullOrEmpty(config.From.Address))
            {
                mailMsg.From = config.From;
            }
            else
            {
                throw new Exception("收件人不能为空！");
            }
            foreach (string item in config.To)
            {
                mailMsg.To.Add(item);
            }
            if (config.Attachments.Count>0)
            {
                foreach (string path in config.Attachments)
                {
                    Attachment file = new Attachment(path);
                    mailMsg.Attachments.Add(file);
                }
            }
            foreach (string item in config.CC)
            {
                mailMsg.CC.Add(item);
            }
            foreach (string item in config.Bcc)
            {
                mailMsg.Bcc.Add(item);
            }
            if (string.IsNullOrEmpty(config.IsBodyHtml) || config.IsBodyHtml == "true")
            {
                mailMsg.IsBodyHtml = true;
            }
            else if (config.IsBodyHtml == "false")
            {
                mailMsg.IsBodyHtml = false;
            }
            else
            {
                throw new Exception("正文格式IsBodyHtml设置错误！");
            }
            mailMsg.Subject = config.Subject;
            mailMsg.SubjectEncoding = config.SubjectEncoding;
            mailMsg.Body = config.Body;
            mailMsg.BodyEncoding = config.BodyEncoding;
            return mailMsg;
        }
    }
}
