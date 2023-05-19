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
using UFIDA.U9.CBO.PubBE.YYC;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using System.DirectoryServices;
using System.Threading;
using System.Net;
using System.IO;

namespace ConsoleTest
{
    class Obj {
        public long ItemID { get; set; }
        public string Name { get; set; }
    }
    class ClassA {
        private string id;
        private string id2;
        private string id3;
        public string id4;
        public string ID { get; set; }
        public decimal d { get; set; }
        public int i { get; set; }
        public ClassB B{get;set;}
        public List<ClassB> BLi { get; set; }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
    class ClassB {
        public string ID { get; set; }
        public decimal d { get; set; }
        public int i { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ClassA c = new ClassA();
            Type t = c.GetType();
            FieldInfo[] fi=GetFields(c.GetType());
            foreach (FieldInfo item in fi)
            {
                Console.WriteLine(item.Name);
            }
         
            Console.ReadLine();
        }
        public static FieldInfo[] GetFields(Type t)
        {
            return t.GetFields();
        }
        public static PropertyInfo[] GetProperties(Type t)
        {
            return t.GetProperties();
        }
        public static MethodInfo[] GetMethods(Type t)
        {
            return t.GetMethods();
        }
        public static MemberInfo[] GetMembers(Type t)
        {
            return t.GetMembers();
        }
        public static Type[] GetNestedTypes(Type t)
        {
            return t.GetNestedTypes();
        }
        public static ConstructorInfo[] GetConstructors(Type t)
        {
            return t.GetConstructors();
        }
        public static string GetHouseInfo()
        {
            //ThreadPool.SetMaxThreads(5, 20);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("Start", 0);
            dic.Add("End", 500);
            dic.Add("Th", 100);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(InsertData), dic);
            Thread td = new Thread(new ParameterizedThreadStart(InsertData));
            td.Start(dic);
            Dictionary<string, int> dic2 = new Dictionary<string, int>();
            dic2.Add("Start", 500);
            dic2.Add("End", 1000);
            dic2.Add("Th", 200);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(InsertData), dic);
            Thread td2 = new Thread(new ParameterizedThreadStart(InsertData));
            td2.Start(dic2);
            //Dictionary<string, int> dic3 = new Dictionary<string, int>();
            //dic3.Add("Start", 600);
            //dic3.Add("End", 900);
            //dic3.Add("Th", 300);
            ////ThreadPool.QueueUserWorkItem(new WaitCallback(InsertData), dic);
            //Thread td3 = new Thread(new ParameterizedThreadStart(InsertData));
            //td3.Start(dic3);

            //Dictionary<string, int> dic4 = new Dictionary<string, int>();
            //dic4.Add("Start", 900);
            //dic4.Add("End", 1200);
            //dic4.Add("Th", 400);
            ////ThreadPool.QueueUserWorkItem(new WaitCallback(InsertData), dic);
            //Thread td4 = new Thread(new ParameterizedThreadStart(InsertData));
            //td4.Start(dic4);

            //Dictionary<string, int> dic5 = new Dictionary<string, int>();
            //dic5.Add("Start", 1200);
            //dic5.Add("End", 1500);
            //dic5.Add("Th", 500);
            ////ThreadPool.QueueUserWorkItem(new WaitCallback(InsertData), dic);
            //Thread td5 = new Thread(new ParameterizedThreadStart(InsertData));
            //td5.Start(dic5);

            //dic = new Dictionary<string, int>();
            //dic.Add("Start", 1500);
            //dic.Add("End", 1600);
            //dic.Add("Th", 500);
            ////ThreadPool.QueueUserWorkItem(new WaitCallback(InsertData), dic);
            //Thread td6 = new Thread(new ParameterizedThreadStart(InsertData));
            //td6.Start(dic);

            return "";
        }
        public static void InsertData(object d)
        {
            Dictionary<string, int> dicW = (Dictionary<string, int>)d;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            HttpHelper httpHelper = new HttpHelper();
            IDataBase db = DBHelperFactory.Create("Default");
            List<string> liSql = new List<string>();
            for (int i = dicW["Start"]; i < dicW["End"]; i++)
            {
                dic = new Dictionary<string, object>();
                dic.Add("district", -1);
                dic.Add("pageSize", 50);
                dic.Add("pageIndex", i);
                Console.WriteLine("Time:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"|"+dicW["Th"].ToString() + ":" + i.ToString());
                //string result = httpHelper.Post("http://zjj.sz.gov.cn/szfdcscjy/esf/publicity/getHouseSourcelibraryList", MyPlatform.Common.JSONUtil.GetJson(dic));
                //Dictionary<string, object> dicResult = MyPlatform.Common.JSONUtil.ParseFromJson<Dictionary<string, object>>(result);
                //if (dicResult["status"].ToString() != "200")
                //{
                //    Thread.Sleep(120000);
                //}
                //Dictionary<string, object> dicResult2 = MyPlatform.Common.JSONUtil.ParseFromJson<Dictionary<string, object>>(dicResult["data"].ToString());
                //List<Dictionary<string, object>> list = MyPlatform.Common.JSONUtil.ParseFromJson<List<Dictionary<string, object>>>(dicResult2["list"].ToString());
                List<Dictionary<string, object>> list = GetDataList(httpHelper, MyPlatform.Common.JSONUtil.GetJson(dic));
                if (list.Count > 0)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        //foreach (string key in list[j].Keys)
                        //{
                        //    string v = list[j][key] == null ? "" : list[j][key].ToString();
                        //    Console.WriteLine(key + ":" + v);
                        //}
                        string sql = string.Format(@"INSERT INTO dbo.sz_houseinfo
        (ID,sedsID,seascId,seabcId,projectName,fullSerialNo,district,buildInArea,houseUsage,houseUsageSplice,floorNo,verifyCode,organName,contractDate,hxType
,hxTypeStr,signDate,averagePriceTotal,status,houseStatus,threadID)VALUES  ( {0} ,{1} ,{2} ,{3} ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}' ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,'{14}'
,'{15}' ,'{16}' ,'{17}' ,'{18}' ,'{19}',{20}      
        )", list[j]["id"], list[j]["sedsId"], list[j]["seascId"], 0, list[j]["projectName"].ToString().Replace('\'',' '), list[j]["fullSerialNo"], list[j]["district"]
        , list[j]["buildInArea"], list[j]["houseUsage"], list[j]["houseUsageSplice"], list[j]["floorNo"], list[j]["verifyCode"], list[j]["organName"]
        , list[j]["contractDate"], list[j]["hxType"], list[j]["hxTypeStr"], list[j]["signDate"], list[j]["averagePriceTotal"], ""
        , list[j]["houseStatus"], dicW["Th"]);
                        liSql.Add(sql);
                    }
                }
                else
                {
                }
                if (liSql.Count > 500)
                {
                    Console.WriteLine("ExecuteTran 500");
                    db.ExecuteTran(liSql);
                    liSql = new List<string>();
                }
            }
            if (liSql.Count > 0)
            {
                Console.WriteLine("ExecuteTran others");
                db.ExecuteTran(liSql);
                liSql = new List<string>();
            }
        }
        public static List<Dictionary<string, object>> GetDataList(HttpHelper http, string json)
        {
            string result = http.Post("http://zjj.sz.gov.cn/szfdcscjy/esf/publicity/getHouseSourcelibraryList", json);
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            Dictionary<string, object> dicResult = MyPlatform.Common.JSONUtil.ParseFromJson<Dictionary<string, object>>(result);
            if (dicResult["status"].ToString() == "200")
            {
                Dictionary<string, object> dicResult2 = MyPlatform.Common.JSONUtil.ParseFromJson<Dictionary<string, object>>(dicResult["data"].ToString());
                list = MyPlatform.Common.JSONUtil.ParseFromJson<List<Dictionary<string, object>>>(dicResult2["list"].ToString());
            }
            else
            {
                Console.WriteLine("Sleep");
                Thread.Sleep(60000);
                list = GetDataList(http, json);
            }
            return list;
        }
        public static string GetCRMCustomer()
        {
            string sql = "";
            try
            {
                int limit = 1000;
                int offset = 0;
                int pageSize = 1000;
                //Dictionary<string, object> dicParam = new Dictionary<string, object>();
                //Dictionary<string, string> dicToken = GetCRMToken();
                //Dictionary<string, object> dicData = new Dictionary<string, object>();//查询对象
                //Dictionary<string, object> dicSearchInfo = new Dictionary<string, object>();//查询条件
                //dicSearchInfo.Add("limit", limit);
                //dicSearchInfo.Add("offset", 0);
                //dicData.Add("dataObjectApiName", "AccountObj");
                //dicData.Add("search_query_info", dicSearchInfo);
                //dicData.Add("find_explicit_total_num", true);//返回total记录数
                //string u = "https://open.fxiaoke.com/cgi/crm/v2/data/query";
                ////参数
                //dicParam.Add("corpAccessToken", dicToken["corpAccessToken"]);
                //dicParam.Add("corpId", dicToken["corpId"]);
                //dicParam.Add("currentOpenUserId", "FSUID_D8A6A93A28FDA27BB086C803174AD5E3");
                //dicParam.Add("data", dicData);
                //MyPlatform.Common.HttpHelper http = new MyPlatform.Common.HttpHelper();
                //string result = http.Post(u, dicParam.ToJson());
                //CRMEntityResult<CRMEntityData> data = result.ToEntity<CRMEntityResult<CRMEntityData>>();
                Dictionary<string, string> dicToken = GetCRMToken();
                CRMUserData userData = GetUserData(1000, 1, dicToken);
                if (userData.errorCode == "0")//获取用户成功
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("begin  ");
                    sb.Append(" delete from CRM_User;");//清空CRM客户
                    for (int i = 0; i < userData.employees.Count; i++)
                    {
                        sb.Append(string.Format("insert into CRM_User values('{0}','{1}');", userData.employees[i].openUserId, userData.employees[i].name));
                    }

                    sb.Append("  end;  ");
                    //new ComMethod(CommonData.getInstance().dicConnect["OACon"], 3000).ExecuteNonQuery(CommandType.Text, sb.ToString(), null);
                }
                else
                {
                    throw new Exception("错误编码：" + userData.errorCode + ",错误信息：" + userData.errorMessage + "," + userData.errorDescription);
                }
                CRMEntityResult<CRMEntityData> data = GetCustomerData(1, 0, dicToken);
                if (data.errorCode == "0")//成功
                {
                    int total = data.data.total;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("begin  ");
                    sb.Append(" delete from crm_Test;");//清空CRM客户
                    if (total > limit)
                    {
                        long times = Convert.ToInt64(Math.Ceiling(Convert.ToDouble(total) / Convert.ToDouble(limit)));
                        for (int i = 0; i < times; i++)
                        {
                            CRMEntityResult<CRMEntityData> resultData = GetCustomerData(limit, offset, dicToken);
                            for (int j = 0; j < resultData.data.dataList.Count; j++)
                            {
                                string owner = "";
                                if (resultData.data.dataList[j].owner != null)
                                {
                                    for (int n = 0; n < resultData.data.dataList[j].owner.Count; n++)
                                    {
                                        owner += resultData.data.dataList[j].owner[n] + ",";
                                    }
                                }
                                sb.Append(string.Format("insert into CRM_Test values('{0}','{1}','{2}','{3}','');", resultData.data.dataList[j]._id, resultData.data.dataList[j].name, resultData.data.dataList[j].high_seas_name, owner.TrimEnd(',')));
                            }
                            offset = limit;
                            limit += pageSize;
                        }
                    }
                    else
                    {
                        CRMEntityResult<CRMEntityData> resultData = GetCustomerData(limit, offset, dicToken);
                        for (int i = 0; i < resultData.data.dataList.Count; i++)
                        {
                            string owner = "";
                            if (resultData.data.dataList[i].owner != null)
                            {
                                for (int n = 0; n < resultData.data.dataList[i].owner.Count; n++)
                                {
                                    owner += resultData.data.dataList[i].owner[n] + ",";
                                }
                            }
                            sb.Append(string.Format("insert into CRM_Test values('{0}','{1}','{2}','{3}','');", resultData.data.dataList[i]._id, resultData.data.dataList[i].name, resultData.data.dataList[i].high_seas_name, owner.TrimEnd(',')));
                        }
                    }
                    sb.Append("  update crm_test t set ownername=(select name from crm_user a where a.id=t.owner) where exists(select 1 from crm_user a where a.id = t.owner) ; ");
                    sb.Append("  end;  ");
                    //new ComMethod(CommonData.getInstance().dicConnect["OACon"], 3000).ExecuteNonQuery(CommandType.Text, sb.ToString(), null);
                    sql = sb.ToString();
                }
                else
                {
                    throw new Exception("错误编码：" + data.errorCode + ",错误信息：" + data.errorMessage + "," + data.errorDescription);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }
        public static CRMUserData GetUserData(int pageSize, int pageNumber, Dictionary<string, string> dicToken)
        {
            try
            {
                //int limit = 1000;
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                string u = "https://open.fxiaoke.com/cgi/user/get/batchByUpdTime";
                //参数
                dicParam.Add("corpAccessToken", dicToken["corpAccessToken"]);
                dicParam.Add("corpId", dicToken["corpId"]);
                dicParam.Add("currentOpenUserId", "FSUID_D8A6A93A28FDA27BB086C803174AD5E3");
                dicParam.Add("pageSize", pageSize);
                dicParam.Add("pageNumber", pageNumber);
                MyPlatform.Common.HttpHelper http = new MyPlatform.Common.HttpHelper();
                string result = http.Post(u, dicParam.ToJson());
                CRMUserData data = JSONUtil.ParseFromJson<CRMUserData>(result);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static CRMEntityResult<CRMEntityData> GetCustomerData(int limit, int offset, Dictionary<string, string> dicToken)
        {
            try
            {
                //int limit = 1000;
                Dictionary<string, object> dicParam = new Dictionary<string, object>();
                Dictionary<string, object> dicData = new Dictionary<string, object>();//查询对象
                Dictionary<string, object> dicSearchInfo = new Dictionary<string, object>();//查询条件
                dicSearchInfo.Add("limit", limit);
                dicSearchInfo.Add("offset", offset);
                dicData.Add("dataObjectApiName", "AccountObj");
                dicData.Add("search_query_info", dicSearchInfo);
                dicData.Add("find_explicit_total_num", true);//返回total记录数
                string u = "https://open.fxiaoke.com/cgi/crm/v2/data/query";
                //参数
                dicParam.Add("corpAccessToken", dicToken["corpAccessToken"]);
                dicParam.Add("corpId", dicToken["corpId"]);
                dicParam.Add("currentOpenUserId", "FSUID_D8A6A93A28FDA27BB086C803174AD5E3");
                dicParam.Add("data", dicData);
                MyPlatform.Common.HttpHelper http = new MyPlatform.Common.HttpHelper();
                string result = http.Post(u, dicParam.ToJson());
                CRMEntityResult<CRMEntityData> data = JSONUtil.ParseFromJson<CRMEntityResult<CRMEntityData>>(result);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 获取CRM系统Token
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCRMToken()
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            try
            {
                string url = "https://open.fxiaoke.com/cgi/corpAccessToken/get/V2";
                Dictionary<string, string> dicAppInfo = new Dictionary<string, string>();
                dicAppInfo.Add("appId", "FSAID_1319d64");//appId
                dicAppInfo.Add("appSecret", "1d87829d97114babb6197ae724e4ede9");//appSecret
                dicAppInfo.Add("permanentCode", "37292998F40B1D6D14B0367B48DDB974");//永久授权码
                MyPlatform.Common.HttpHelper http = new MyPlatform.Common.HttpHelper();
                //string result = HttpMethod.DoPost(url, "post", dicAppInfo.ToJson());
                string result = http.Post(url, dicAppInfo.ToJson());
                dicResult = JSONUtil.ParseFromJson<Dictionary<string, string>>(result);
                if (dicResult["errorCode"] != "0")
                {
                    if (dicResult["errorCode"] == "-1")//系统繁忙重新发起请求
                    {
                        dicResult = GetCRMToken();
                    }
                    else
                    {
                        throw new Exception("ErrorCode:" + dicResult["errorCode"] + ",ErrorMessage" + dicResult["errorMessage"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dicResult;
        }
        public static void Addfund()
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "fund_basic");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("market", "");
            dicParam.Add("status", "");
            //startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,name,management,custodian,fund_type,found_date,due_date,list_date,issue_date,delist_date,issue_amount,m_fee,c_fee,duration_year,p_value,min_amount,exp_return,benchmark,status,invest_type,type,trustee,purc_startdate,redm_startdate");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex values('600362.SH','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ",'" + ri.data.items[i][11] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
            //if (startDate < DateTime.Now)
            //{
            //    AddDailyBasic(startDate.AddDays(1));
            //}
        }
        public static void AddDailyBasic2()
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "daily_basic");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ts_code", "600362.SH");
            dicParam.Add("trade_date", "");
            dicParam.Add("start_date", "");
            dicParam.Add("end_date", "");
            //startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex values('600362.SH','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ",'" + ri.data.items[i][11] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
            //if (startDate < DateTime.Now)
            //{
            //    AddDailyBasic(startDate.AddDays(1));
            //}
        }

        public static void AddAPIInfo(string html)
        {
            List<string> li = new List<string>();//Sqls
            //标题
            Regex regTitle = new Regex("(?<=<h2.*>).*(?=</[\\s\\S]*?h2 >)");
            //API名称
            Regex regApi = new Regex("(?<=接口：).*?(?=<br>)");
            Regex regDescpri = new Regex("(?<=描述：).*?(?=<br>)");
            Match m = regTitle.Match(html);
            Match m1 = regApi.Match(html);
            Match m2 = regDescpri.Match(html);
            string title = m.Value;
            string api = m1.Value;
            string Descprition = m2.Value;
            Console.WriteLine("title:" + title);
            Console.WriteLine("api:" + api);
            Console.WriteLine("Descprition:" + Descprition);
            Console.WriteLine("如果有问题，请输入\"N\"终止");
            string sql = "insert into Sys_APIs values('" + m.Value + "','" + m1.Value + "','" + m2.Value + "')";
            string result = Console.ReadLine();
            if (result.ToUpper() == "N")
            {
                return;
            }
            sql += "; select SCOPE_IDENTITY()";
            IDataBase db = new SqlServerDataBase();
            object o = db.ExecuteScalar(sql);
            int apiID = int.Parse(o.ToString());
            Console.WriteLine("结果:" + o.ToString());


            //Input Html
            Match m3 = new Regex("输入参数[\\s\\S]*?输出参数").Match(html);
            string input = m3.Value;
            //输入参数            
            MatchCollection m4 = new Regex("(?<=<td.*>).*(?=</td>)").Matches(input);
            IEnumerator e = m4.GetEnumerator();
            int i = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert into API_Input (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values(");
            while (e.MoveNext())
            {
                if (i % 4 == 0 && i != 0)
                {
                    li.Add(sb.ToString().Substring(0, sb.ToString().Length - 1) + "," + apiID.ToString() + "," + (i * 10).ToString() + ")");
                    sb = new StringBuilder();
                    sb.Append("Insert into API_Input (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values(");
                }
                sb.Append(" '" + e.Current.ToString() + "',");
                i++;
            }

            //输出参数Html
            Match m5 = new Regex("输出参数[\\s\\S]*?</table>").Match(html);
            string output = m5.Value;
            //输出参数
            MatchCollection m6 = new Regex("(?<=<td.*>).*(?=</td>)").Matches(output);
            IEnumerator e2 = m6.GetEnumerator();
            sb = new StringBuilder();
            sb.Append("Insert into API_OutPut (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values (");
            i = 0;
            while (e2.MoveNext())
            {
                if (i % 4 == 0 && i != 0)
                {
                    li.Add(sb.ToString().Substring(0, sb.ToString().Length - 1) + "," + apiID.ToString() + "," + (i * 10).ToString() + ")");
                    sb = new StringBuilder();
                    sb.Append("Insert into API_OutPut (ParamName ,ParamType,IsRequired,Descprition,ApiID, OrderNo) values (");
                }
                sb.Append(" '" + e2.Current.ToString() + "',");
                i++;
            }
            db.ExecuteTran(li);
        }

        public static void AddBonus(string ts_code)
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "dividend");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("ts_code", ts_code);
            dicParam.Add("ann_date", "");
            dicParam.Add("record_date", "");
            dicParam.Add("ex_date", "");
            dicParam.Add("imp_ann_date", "");
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,end_date,ann_date,div_proc,stk_div,stk_bo_rate,stk_co_rate,cash_div,cash_div_tax,record_date,ex_date,pay_date,div_listdate,imp_ann_date,base_date,base_share");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_Bonus values('" + ts_code + "','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ",'" + ri.data.items[i][11] + "'";
                sql += ",'" + ri.data.items[i][12] + "'";
                sql += ",'" + ri.data.items[i][13] + "'";
                sql += ",'" + ri.data.items[i][14] + "'";
                sql += ",'" + ri.data.items[i][15] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
        }

        public static void AddDaily(DateTime startDate)
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "index_daily");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("trade_date", "");
            dicParam.Add("ts_code", "399006.SZ");
            dicParam.Add("start_date", startDate.ToString("yyyyMMdd"));
            dicParam.Add("end_date", startDate.AddDays(3000).ToString("yyyyMMdd"));
            startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,trade_date,close,open,high,low,pre_close,change,pct_chg,vol,amount");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex_Daily values('399006.SZ','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
            if (startDate < DateTime.Now)
            {
                AddDaily(startDate.AddDays(1));
            }
        }
        public static void AddDailyBasic(DateTime startDate)
        {
            HttpHelper hh = new HttpHelper();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("api_name", "index_dailybasic");
            dic.Add("token", "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa");
            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add("trade_date", "");
            dicParam.Add("ts_code", "600362.SH");
            dicParam.Add("start_date", startDate.ToString("yyyyMMdd"));
            dicParam.Add("end_date", startDate.AddDays(3000).ToString("yyyyMMdd"));
            startDate = startDate.AddDays(3000);
            dic.Add("params", dicParam);
            dic.Add("fields", "ts_code,trade_date,total_mv,float_mv,total_share,float_share,free_share,turnover_rate,turnover_rate_f,pe,pe_ttm,pb");
            string strJson = JsonHelper.GetJsonJS(dic);
            string result = hh.Post("http://api.tushare.pro", strJson);
            ResultInfo ri = JsonHelper.JsonDeserializeJS<ResultInfo>(result);
            IDataBase db = new SqlServerDataBase();
            List<string> liSql = new List<string>();
            string sql = "";
            for (int i = 0; i < ri.data.items.Count; i++)
            {
                sql = "insert into Base_MarketIndex values('600362.SH','" + ri.data.items[i][1] + "'";
                sql += ",'" + ri.data.items[i][2] + "'";
                sql += ",'" + ri.data.items[i][3] + "'";
                sql += ",'" + ri.data.items[i][4] + "'";
                sql += ",'" + ri.data.items[i][5] + "'";
                sql += ",'" + ri.data.items[i][6] + "'";
                sql += ",'" + ri.data.items[i][7] + "'";
                sql += ",'" + ri.data.items[i][8] + "'";
                sql += ",'" + ri.data.items[i][9] + "'";
                sql += ",'" + ri.data.items[i][10] + "'";
                sql += ",'" + ri.data.items[i][11] + "'";
                sql += ")";
                liSql.Add(sql);
            }
            db.ExecuteTran(liSql);
            if (startDate < DateTime.Now)
            {
                AddDailyBasic(startDate.AddDays(1));
            }
        }

    }

    class ResultInfo
    {
        public string request_id { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public Data2 data { get; set; }
        public bool has_more { get; set; }

    }
    class Data2
    {
        public string[] fields { get; set; }
        public List<string[]> items { get; set; }

    }

    public class CRMCustomer
    {
        public string name { get; set; }
        public string _id { get; set; }
        public string high_seas_name { get; set; }
        public List<string> owner { get; set; }
    }
    public class CRMEntityData
    {
        public List<CRMCustomer> dataList { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
    }

    public class CRMEntityResult<T> where T : class
    {
        public string errorDescription { get; set; }
        public string errorMessage { get; set; }
        public string errorCode { get; set; }
        public T data { get; set; }
    }
    public class CRMUserData
    {
        public string errorDescription { get; set; }
        public string errorMessage { get; set; }
        public string errorCode { get; set; }
        public List<CRMUser> employees { get; set; }
    }
    public class CRMUser
    {
        public string openUserId { get; set; }
        public string name { get; set; }

    }
}
