using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace MyPlatform.Common.Mail
{
    public class MailHelper
    {
        // 邮件配置和信息
        public MailConfig config { get; set; }
        public SmtpClient client { get; set; }
        public MailHelper()
        {
            config = new MailConfig();
            client = new SmtpClient();
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        public void SendMail()
        {
            try
            {
                MailMessage mail = GetMail();
                GetConfig();
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetConfig()
        {
            client.Host = config.Host;
            if (config.Port!=0)
            {
                client.Port = config.Port;
            }
            client.Credentials = new NetworkCredential(config.Email, config.Password);
        }
        public void SendAsyncCancel()
        {
            GetConfig();
            client.SendAsyncCancel();
        }

        public void SendMailAsync(SendCompletedEventHandler handler, object o)
        {
            try
            {
                MailMessage mail = GetMail();
                //TODO:配置在web.config中
                GetConfig();
                if (handler != null)
                {
                    client.SendCompleted += handler;
                }
                
                client.SendAsync(mail, o);
                //client.SendAsyncCancel();
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
            Regex reg = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
            if (config.From != null)
            {
                if (reg.IsMatch(config.From.Address))
                {
                    mailMsg.From = config.From;
                }
                else
                {
                    throw new Exception("收件人地址【" + config.From.Address + "】不是正确的邮箱地址！");
                }
            }
            else
            {
                throw new Exception("收件人地址未配置！");
            }
            if (config.To != null)
            {

                foreach (string item in config.To)
                {
                    if (reg.IsMatch(item))
                    {
                        mailMsg.To.Add(item);
                    }
                    else
                    {
                        throw new Exception("收件人【" + item + "】不是正确的邮箱地址");
                    }
                }
            }
            else
            {
                throw new Exception("收件人地址未配置！");
            }
            //TODO：完善附件邮件发送
            if (config.Attachments.Count > 0)
            {
                foreach (string path in config.Attachments)
                {
                    Attachment file = new Attachment(path);
                    mailMsg.Attachments.Add(file);
                }
            }
            if (config.CC != null)
            {
                foreach (string item in config.CC)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (reg.IsMatch(item))
                        {
                            mailMsg.CC.Add(item);
                        }
                        else
                        {
                            throw new Exception("抄送人【" + item + "】不是正确的邮箱地址！");
                        }
                    }

                }
            }
            if (config.Bcc != null)
            {
                foreach (string item in config.Bcc)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (reg.IsMatch(item))
                        {
                            mailMsg.Bcc.Add(item);
                        }
                        else
                        {
                            throw new Exception("密送人【" + item + "】不是正确的邮箱地址！");
                        }
                    }

                }
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
