using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MyPlatform.Common.Mail
{
    public class MailConfig
    {
        /// <summary>
        /// 用于 SMTP 事务的主机的名称或 IP 地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public MailAddress From { get; set; }
        /// <summary>
        /// 收件人集合
        /// </summary>
        public ArrayList To { get; set; }
        /// <summary>
        /// 抄送人集合
        /// </summary>
        public ArrayList CC { get; set; }
        /// <summary>
        /// 密送人集合
        /// </summary>  
        public ArrayList Bcc { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 邮件正文是否是Html格式，通过设置True/False来改变邮件正文格式，默认为True
        /// </summary>
        public string IsBodyHtml { get; set; }
        /// <summary>
        /// 标题编码格式
        /// </summary>
        public Encoding SubjectEncoding { get; set; }
        /// <summary>
        /// 邮件正文编码格式
        /// </summary>
        public Encoding BodyEncoding { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public List<string> Attachments { get; set; }
    }
}
