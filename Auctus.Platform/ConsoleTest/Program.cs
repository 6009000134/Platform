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
            Type[] types = typeof(Program).Assembly.GetTypes();
            List<string> li = new List<string>();
            List<string> li2 = new List<string>();
            List<string> li3 = new List<string>();
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].BaseType != null)
                {
                    if (types[i].BaseType.FullName.Contains("H"))
                    {
                        li.Add(types[i].BaseType.FullName);
                    }
                    else
                    {
                        li2.Add(types[i].BaseType.FullName);
                    }
                }
                else
                {
                    li3.Add(types[i].FullName);
                }
             
            }
            Console.WriteLine("工单：");
            for (int i = 0; i < li.Count; i++)
            {
                Console.WriteLine(li[i]);
            }

            Console.WriteLine("非工单");
            for (int i = 0; i < li2.Count; i++)
            {
                Console.WriteLine(li2[i]);
            }
            Console.WriteLine("无继承类");
            for (int i = 0; i < li3.Count; i++)
            {
                Console.WriteLine(li3[i]);
            }
            Console.ReadLine();
            decimal a = 0.115m;
            decimal b = 567;
            decimal sss = a * b;
            decimal ss=Math.Round(a*b, 2);
            decimal ss2=decimal.Round(65.205m, 2, MidpointRounding.AwayFromZero);
            ClassA ca = new ClassA();
            ClassB cb = new ClassB();
            Console.WriteLine(ca.GetType().BaseType);
            Console.WriteLine(cb.GetType().BaseType);
            Type t = a.GetType();
            Console.WriteLine(t.BaseType);
            Console.WriteLine(t.FullName);
            Console.WriteLine(t.Name);
            Console.WriteLine(t.Namespace);
            Console.WriteLine(t.GetProperties());

            Console.WriteLine(t.Assembly.FullName);
            Console.WriteLine(t.Assembly.CodeBase);
            Console.WriteLine(t.Assembly.Location);
            AssemblyName an = t.Assembly.GetName();
            Console.WriteLine(an.CultureName);
            Console.WriteLine(an.FullName);
            Console.WriteLine(an.Name);
            Console.WriteLine(an.Version);
            Activator.CreateInstance(t);
            Console.ReadLine();
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
