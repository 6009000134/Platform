using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using JWT.Serializers;
using MyPlatform.Common;
using MyPlatform.DBUtility;
using MyPlatform.SQLServerDAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using System.DirectoryServices;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ConsoleTest
{

    class Program
    {
        static void Main(string[] args)
        {
            //域登录
            //string userAccount = "liufei";
            //DirectoryEntry du = new DirectoryEntry(@"LDAP://auctus.cn", userAccount, "Qwelsy@123");
            //DirectorySearcher src = new DirectorySearcher(du);
            //src.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + userAccount + "))";
            //src.PropertiesToLoad.Add("cn");
            //src.SearchRoot = du;
            //src.SearchScope = SearchScope.Subtree;
            //SearchResult result = src.FindOne();
            //Auctus.CustomSV.AP_PayBill.CustPayBill
            //Type[] types = typeof(Auctus.CustomSV.CustFA).Assembly.GetTypes();
            string s = "";
            while (s != "3") {
                s = Console.ReadLine();
                if (s == "1")
                {
                    int i = 0;
                    while (s == "1")
                    {
                        i += 1;
                        if (i <= 5) { 
                        Send();
                    }
                        if (i > 5) {
                            break;
                        }
                    }                    
                }
                else if (s == "2") {
                    SendMailAsync(mailSent);
               
                }
            }

        }
        private static bool mailSent=false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
        public static void Send()
        {
            MyPlatform.Common.Mail.MailHelper mailHelper = new MyPlatform.Common.Mail.MailHelper();
            mailHelper.config.From = new System.Net.Mail.MailAddress("sys_sup@auctus.cn", "深圳力同芯科技发展有限公司");
            mailHelper.config.Email = "sys_sup@auctus.cn";
            mailHelper.config.Password = "Qwelsy@1234";
            mailHelper.config.Host = "192.168.1.1";
            mailHelper.config.IsBodyHtml = "true";
            mailHelper.config.To.Add("491675469@qq.com");
            mailHelper.config.Subject = "测试";
            mailHelper.config.Body = "<h1>测试邮件</h1>";
            mailHelper.SendMail();
        }
        public static void SendMailAsync(object o)
        {
            MyPlatform.Common.Mail.MailHelper mailHelper = new MyPlatform.Common.Mail.MailHelper();
            mailHelper.config.From = new System.Net.Mail.MailAddress("sys_sup@auctus.cn", "深圳力同芯科技发展有限公司");
            mailHelper.config.Email = "sys_sup@auctus.cn";
            mailHelper.config.Password = "Qwelsy@1234";
            mailHelper.config.Host = "192.168.1.1";
            mailHelper.config.IsBodyHtml = "true";
            mailHelper.config.To.Add("491675469@qq.com");
            mailHelper.config.Subject = "测试";
            mailHelper.config.Body = "<h1>测试邮件Async</h1>";
            mailHelper.SendMailAsync(SendCompletedCallback,"subject");
            Console.WriteLine("输入c开头，撤销邮件发送");
            string isCancel = Console.ReadLine();
            if (isCancel == "c")
            {
                mailHelper.SendAsyncCancel();
            }
        }
        public static string GetJson<T>(T obj)
        {
            IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
            isoDateTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonConverter[]
            {
                isoDateTimeConverter
            });
        }

    }

    class ClassA
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set;}
    }
    class ClassB:ClassA
    {
        public string Song { get; set; }
    }



}
