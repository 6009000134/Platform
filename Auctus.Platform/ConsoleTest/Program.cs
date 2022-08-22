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
using CRMUtility;
using UFIDA.U9.CBO.PubBE.YYC;

namespace ConsoleTest
{

    class Program
    {
        static void Main(string[] args)
        {
            decimal d = new decimal(123.00000);
            string s = d.ToString("N");
            List<string> li = new List<string>();
            li.Add("aa");
            li.Add("bb");
            li.Add("cc");
            Console.WriteLine(li.ToString());
            Console.ReadLine();
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


            //Dictionary<string, string> dicToken = CRMUtils.GetToken();
            //Dictionary<string, object> dicParam = CRMUtils.SetBaseParam(dicToken);
            //dicParam.Add("apiName", "AccountObj");
            //dicParam.Add("includeDetail", false);
            //string result= CRMUtils.Post("https://open.fxiaoke.com/cgi/crm/v2/object/describe", dicParam.ToJson());
            //Dictionary<string, object> dicResult = JSONUtil.ParseFromJson<Dictionary<string,object>>(result);
            //Dictionary<string,object> dicData= JSONUtil.ParseFromJson<Dictionary<string, object>>(dicResult["data"].ToString());
            //Dictionary<string,object> dicData2= JSONUtil.ParseFromJson<Dictionary<string, object>>(dicData["describe"].ToString());
            //Dictionary<string, object> dicData3 = JSONUtil.ParseFromJson<Dictionary<string, object>>(dicData2["fields"].ToString());
            //Dictionary<string, object> dicData4 = JSONUtil.ParseFromJson<Dictionary<string, object>>(dicData3["UDSSel3__c"].ToString());
            //List<Dictionary<string,string>> dicData5 = JSONUtil.ParseFromJson<List<Dictionary<string, string>>>(dicData4["options"].ToString());

            //Console.WriteLine("请输入视图名称：");
            //string con1 = Console.ReadLine();
            //DBHelper helper=DALAccess.GetDB(con1);
            //helper.Execute();


        }
    }
    public class DBHelper
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public void GetConn()
        {

        }
        public string Execute()
        {
            return "";
        }
    }
    public class DALAccess
    {
        private static Dictionary<string, List<DBHelper>> dicDBs = new Dictionary<string, List<DBHelper>>();
        //连接字符是动态的，静态变量
        //public static string DataSource = "db";
        public SqlConnection con;
        public SqlCommand cmd;


        private DALAccess(string DataSource)
        {
            con = new SqlConnection(DataSource);
            con.Open();
        }


        public static DBHelper GetDB(string con)
        {
            if (dicDBs.ContainsKey(con))
            {
                return dicDBs[con][0];
            }
            else
            {
                return CreateDBHelper(con);
            }
        }

        public static DBHelper CreateDBHelper(string con)
        {
            DBHelper h = new DBHelper();
            Console.WriteLine("创建连接：" + con);
            return h;
        }

    }

    /// <summary>
    /// 皇帝
    /// </summary>
    public class Huangdi
    {
        private static Huangdi huangdi = new Huangdi();
        public Huangdi()
        {

        }
        public static Huangdi GetInstance()
        {
            return huangdi;
        }
    }

    public class Dachen
    {

    }



}
