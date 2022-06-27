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

            /*
         1、根据view查询constring
         2、获取db operator
         */
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("ID");
            DataColumn dc2 = new DataColumn("ID2");
            dt.Columns.Add(dc);
            dt.Columns.Add(dc2);
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i * 10;
                dt.Rows.Add(dr);
            }
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i * 10;
                dt.Rows.Add(dr);
            }
            DataTable dt2 = dt.AsDataView().ToTable(true, "ID");

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
