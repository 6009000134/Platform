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
            MyPlatform.Model.ReturnData result = new MyPlatform.Model.ReturnData();
            result.S = true;
            ClassA ca = new ClassA();
            ca.Name = "123";
            ca.CreatedDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            result.D = ca;
            string str = GetJson<MyPlatform.Model.ReturnData>(result);

            Console.ReadLine();
            decimal a = 0.115m;
            decimal b = 567;
            decimal sss = a * b;
            decimal ss=Math.Round(a*b, 2);
            decimal ss2=decimal.Round(65.205m, 2, MidpointRounding.AwayFromZero);

            Type t = a.GetType();
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


}
