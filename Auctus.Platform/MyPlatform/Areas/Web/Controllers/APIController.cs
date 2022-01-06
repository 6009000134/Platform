using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace MyPlatform.Areas.Web.Controllers
{
    /// <summary>
    /// tushare的API接口
    /// </summary>
    public class APIController : ApiController
    {
        //TODO：更新接口每日数据
        MyPlatform.BLL.Sys_ApiBLL bll = new MyPlatform.BLL.Sys_ApiBLL();
        string apiToken = "8637e17fe4cbc18f2c412229ad41f8628d0849598c73e4fd3332d8fa";
        string apiUrl = "http://api.tushare.pro";
        /// <summary>
        /// 创建接口参数
        /// </summary>
        /// <param name="apiID">接口ID</param>
        /// <param name="dicInput">传入参数及值</param>
        /// <returns></returns>
        public APIInputParamModel CreateInputStr(int apiID, Dictionary<string, string> dicInput)
        {

            APIInputParamModel input = new APIInputParamModel();
            input.token = apiToken;//设置Token
            input.@params = new Dictionary<string, string>();
            DataSet ds = bll.GetDetail(apiID);//获取api接口信息
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    input.api_name = ds.Tables[0].Rows[0]["ApiName"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (dicInput.Count > 0)//前端传参
                    {
                        foreach (KeyValuePair<string, string> item in dicInput)//校验前端传入的输入参数是否全部存在
                        {
                            if (ds.Tables[1].Select("ParamName='" + item.Key + "'").Count() == 0)
                            {
                                throw new Exception("接口不存在" + item.Key + "输入参数，请核对接口输入参数信息！");
                            }
                        }
                        foreach (DataRow dr in ds.Tables[1].Rows)//设置传入参数及其值
                        {
                            if (dicInput.Keys.Contains(dr["ParamName"].ToString()))
                            {
                                input.@params.Add(dr["ParamName"].ToString(), dicInput[dr["ParamName"].ToString()]);
                            }
                            else
                            {
                                input.@params.Add(dr["ParamName"].ToString(), "");
                            }
                        }
                    }
                    else//无前端传参
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            input.@params.Add(dr["ParamName"].ToString(), "");
                        }
                    }
                }
                else
                {
                    throw new Exception("未找到API接口的输入参数信息");
                }
                //设置输出fields
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        input.fields += dr["ParamName"].ToString() + ",";
                    }
                    input.fields = input.fields.TrimEnd(',');
                }
                else
                {
                    throw new Exception("未找到API接口的输出参数信息");
                }
            }
            return input;
        }
        #region 获取Tushare接口数据


        /// <summary>
        /// 获取API结果集
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetApiResult([FromBody]JObject postData)
        {
            ReturnData result = new ReturnData();
            try
            {
                int apiID = Convert.ToInt32(postData.GetValue("apiID"));
                string url = postData.GetValue("url").ToString();
                string body = postData.GetValue("postData").ToJson();
                Model.Sys_APIModel apiInfo = bll.GetApiInfo(apiID);
                TuShareResult apiResult;
                switch (apiInfo.ApiName)
                {
                    case "index_member":// 行业成分股，按行业循环获取
                        List<TuShareResult> li = GetIndexMember(url, body, 11, apiID);
                        for (int i = 0; i < li.Count; i++)
                        {
                            if (li[i].data.items.Count > 0)
                            {
                                result.S = InsertApiResult(li[i], apiID);
                            }
                        }
                        break;
                    case "daily_basic":
                    case "daily":
                        DataTable dt = bll.GetNoDataCalendar("SSE");
                        int colNum = 2000;
                        DataSet ds=SplitDataTable(dt, colNum);
                        int tableNum = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dt.Rows.Count) / Convert.ToDouble(colNum)));
                        ThreadPool.SetMaxThreads(5, 20);
                        object o = new object();
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            Dictionary<string, object> dicObj = new Dictionary<string, object>();
                            dicObj.Add("dt",ds.Tables[i]);
                            dicObj.Add("apiID",apiID);
                            dicObj.Add("url", url);
                            dicObj.Add("ID", "Thread-2021-"+i.ToString());
                            //if (i==0)
                            //{
                            //    InsertDailyList(dicObj);
                            //}
                            ThreadPool.QueueUserWorkItem(new WaitCallback(InsertDailyList), dicObj);
                            //System.Threading.Tasks.Task task = new System.Threading.Tasks.Task();
                        }
                        //InsertDailyList
                        //按日期获取信息
                        //BLL.QueryBLL queryBLL = new BLL.QueryBLL();
                        //DataSet ds = queryBLL.Query(3003);//交易日历
                        //DataRow[] drs = ds.Tables[0].Select("is_open=1");

                        break;
                    default:
                        apiResult = Post(url, body);
                        result.S = InsertApiResult(apiResult, apiID);
                        break;
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        public DataSet SplitDataTable(DataTable dt, int colNum)
        {
            DataSet ds = new DataSet();
            if (dt.Rows.Count > 0)
            {
                int tableNum = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dt.Rows.Count) / Convert.ToDouble(colNum)));
                DataTable[] dtArr = new DataTable[tableNum];
                for (int i = 0; i < tableNum; i++)
                {
                    dtArr[i] = dt.Clone();
                    dtArr[i].TableName = "Table" + i.ToString();
                    if (i != tableNum - 1)
                    {
                        for (int j = i * colNum; j < (i + 1) * colNum; j++)
                        {
                            dtArr[i].ImportRow(dt.Rows[j]);
                        }                      
                    }
                    else
                    {
                        for (int j = i * colNum; j < dt.Rows.Count; j++)
                        {
                            dtArr[i].ImportRow(dt.Rows[j]);
                        }
                    }
                    ds.Tables.Add(dtArr[i]);
                }
            }
            return ds;
        }
        public void InsertDailyList(object o)
        {
            //DataTable dt, int apiID, string url;
            //dt = o.dt;
            Dictionary<string, object> dicObj = (Dictionary<string, object>)o;
            DataTable dt = (DataTable)dicObj["dt"];
            int apiID = (int)dicObj["apiID"];
            string url = (string)dicObj["url"];
            string threadID = (string)dicObj["ID"];
            ReturnData result = new ReturnData();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("trade_date", dt.Rows[i]["cal_date"].ToString());
                dic.Add("ts_code", "");
                dic.Add("start_date", "");
                dic.Add("end_date", "");
                Common.LogHelper.Default.WriteInfo("Thread:"+threadID);
                APIInputParamModel inputParam = CreateInputStr(apiID, dic);
                TuShareResult tempResult = MultiPost(url, inputParam.ToJson<APIInputParamModel>(),threadID);
                if (tempResult.data.items.Count > 0)
                {
                    result.S = InsertApiResult(tempResult, apiID);
                }
            }
            //return result;
        }
        public bool InsertApiResult(TuShareResult result, int apiID)
        {
            if (result.data == null)
            {
                throw new Exception("API接口无数据返回");
            }
            else if (result.data.has_more)
            {
                throw new Exception("未获取到全部数据");
            }
            return bll.GetApiResult(result, apiID).S;
        }
        /// <summary>
        /// 获取行业成分数据
        /// </summary>
        /// <param name="url">tushare接口url</param>
        /// <param name="body">tushare输入参数</param>
        /// <param name="inputApiID">输入参数数据所属API ID</param>
        /// <param name="apiID">存放API数据表apiID</param>
        /// <returns></returns>
        public List<TuShareResult> GetIndexMember(string url, string body, int inputApiID, int apiID)
        {
            DataSet ds = bll.GetApiData(inputApiID);
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("未找到行业数据！");
            }
            Dictionary<string, string> dic;
            List<TuShareResult> li = new List<TuShareResult>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //TODO:目前只取申万一级行业数据
                if (ds.Tables[0].Rows[i]["level"].ToString() == "L1")
                {
                    //最新
                    dic = new Dictionary<string, string>();
                    dic.Add("index_code", ds.Tables[0].Rows[i]["index_code"].ToString());
                    dic.Add("ts_code", "");
                    dic.Add("is_new", "Y");
                    APIInputParamModel inputParam = CreateInputStr(apiID, dic);
                    TuShareResult tempResult = Post(url, inputParam.ToJson<APIInputParamModel>());
                    li.Add(tempResult);
                    //非最新
                    dic = new Dictionary<string, string>();
                    dic.Add("index_code", ds.Tables[0].Rows[i]["index_code"].ToString());
                    dic.Add("ts_code", "");
                    dic.Add("is_new", "N");
                    APIInputParamModel inputParamN = CreateInputStr(apiID, dic);
                    TuShareResult tempResultN = Post(url, inputParamN.ToJson<APIInputParamModel>());
                    li.Add(tempResultN);
                }
            }
            return li;
        }


        private object lockObj = new object();
        public void GetDividend(object obj)
        {
            Dictionary<string, object> dic = (Dictionary<string, object>)obj;
            int apiID = (int)dic["apiID"];
            APIInputParamModel inputParam = (APIInputParamModel)dic["inputParam"];
            DataTable dt = (DataTable)dic["dt"];
            TuShareResult apiResultInsert = new TuShareResult();
            apiResultInsert.data = new TuShareResultData();
            apiResultInsert.data.items = new List<List<string>>();
            int index = 1;
            int min = DateTime.Now.Minute;
            int sec = DateTime.Now.Second;
            foreach (DataRow dr in dt.Rows)
            {
                if (index % 60 == 0)
                {
                    int min2 = DateTime.Now.Minute;
                    int sec2 = DateTime.Now.Second;

                    if (min2 - min == 1)
                    {
                        if (60 - sec + sec2 < 60)
                        {
                            Thread.Sleep((Math.Abs(sec2 - sec)) * 1000);
                        }
                    }
                    else if (min2 - min == 0)
                    {
                        Thread.Sleep(Math.Abs(60 - sec2 + sec) * 1000);
                    }
                    min = min2;
                    sec = sec2;
                }
                index += 1;
                HttpHelper hh = new HttpHelper();
                inputParam.@params["ts_code"] = dr["ts_code"].ToString();
                //postData.
                TuShareResult apiResult;
                string inputStr = "";
                lock (this)
                {
                    inputStr = inputParam.ToJson<APIInputParamModel>();
                }
                string responStr = hh.Post(apiUrl, inputStr);
                apiResult = JSONUtil.ParseFromJson<TuShareResult>(responStr);
                if (apiResult.data != null)//API接口无返回数据
                {
                    if (apiResultInsert.data.items.Count > 3000)
                    {
                        apiResultInsert.data.items.AddRange(apiResult.data.items);
                        bll.GetApiResult(apiResultInsert, apiID);
                        apiResultInsert.data.items = new List<List<string>>();
                    }
                    else
                    {
                        apiResultInsert.data.items.AddRange(apiResult.data.items);
                    }

                }
            }
            if (apiResultInsert.data.items.Count > 0)
            {
                bll.GetApiResult(apiResultInsert, apiID);
            }

        }
        /// <summary>
        /// 多线程获取分红数据
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetDividendResultMultiple([FromBody]Dictionary<string, object> dic)
        {
            int apiID = Convert.ToInt32(dic["apiID"]);
            APIInputParamModel inputParam = CreateInputStr(apiID, new Dictionary<string, string>());
            ReturnData result = new ReturnData();
            try
            {
                DataTable dt = bll.GetTsCode(dic["ts_code"].ToString()).Tables[0];
                TuShareResult apiResultInsert = new TuShareResult();
                apiResultInsert.data = new TuShareResultData();
                apiResultInsert.data.items = new List<List<string>>();
                if (dt.Rows.Count > 0)
                {
                    int count = dt.Rows.Count;
                    int thCount = count % 2 == 0 ? 2 : 3;
                    int perThCount = count / 2;
                    for (int i = 1; i < thCount + 1; i++)
                    {
                        Dictionary<string, object> dicTh = new Dictionary<string, object>();
                        dicTh.Add("apiID", apiID);
                        DataTable dtNew = new DataTable();
                        dtNew = dt.Copy();
                        dtNew.Clear();
                        for (int j = perThCount * (i - 1); j < i * perThCount; j++)
                        {
                            dtNew.Rows.Add(dt.Rows[j].ItemArray);
                        }
                        dicTh.Add("dt", dtNew);
                        dicTh.Add("inputParam", inputParam);
                        Thread td = new Thread(new ParameterizedThreadStart(GetDividend));
                        td.Start(dicTh);
                    }
                }
                else
                {
                    result.SetErrorMsg("找不到股票代码数据！");
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取分红数据
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetDividendResult([FromBody]Dictionary<string, object> dic)
        {
            int apiID = Convert.ToInt32(dic["apiID"]);
            APIInputParamModel inputParam = CreateInputStr(apiID, new Dictionary<string, string>());
            ReturnData result = new ReturnData();
            try
            {
                DataTable dt = bll.GetTsCode(dic["ts_code"].ToString()).Tables[0];
                TuShareResult apiResultInsert = new TuShareResult();
                apiResultInsert.data = new TuShareResultData();
                apiResultInsert.data.items = new List<List<string>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpHelper hh = new HttpHelper();
                        inputParam.@params["ts_code"] = dr["ts_code"].ToString();
                        //postData.
                        TuShareResult apiResult = JSONUtil.ParseFromJson<TuShareResult>(hh.Post(apiUrl, inputParam.ToJson<APIInputParamModel>()));
                        if (apiResult.data == null)//API接口无返回数据
                        {
                            result.S = false;
                            result.M = "API接口无数据返回！";
                        }
                        else
                        {
                            if (apiResultInsert.data.items.Count > 3000)
                            {
                                //foreach (List<string> item in apiResult.data.items)
                                //{
                                //    apiResultInsert.data.items[0].AddRange(item);
                                //}
                                apiResultInsert.data.items.AddRange(apiResult.data.items);
                                result = bll.GetApiResult(apiResultInsert, apiID);
                                apiResultInsert.data.items = new List<List<string>>();
                            }
                            else
                            {
                                apiResultInsert.data.items.AddRange(apiResult.data.items);
                            }

                        }
                    }
                    if (apiResultInsert.data.items.Count > 0)
                    {
                        result = bll.GetApiResult(apiResultInsert, apiID);
                    }

                }
                else
                {
                    result.SetErrorMsg("找不到股票代码数据！");
                }
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        #endregion

        #region API信息增删改查及相关操作
        /// <summary>
        /// 根据API信息，创建表
        /// </summary>
        /// <param name="apiID"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CreateApiTable([FromBody]int apiID)
        {
            ReturnData result = new ReturnData();
            try
            {
                result = bll.CreateApiTable(apiID);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取API详情
        /// </summary>
        /// <param name="apiID">API ID</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetDetail([FromBody]int apiID)
        {
            ReturnData result = new ReturnData();
            try
            {
                result.S = true;
                result.D = bll.GetDetail(apiID);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 新增API
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Add(Dictionary<object, object> dic)
        {
            ReturnData result = new ReturnData();
            try
            {
                dic["inputParam"] = ((Newtonsoft.Json.Linq.JArray)dic["inputParam"]).ToObject<string[]>();
                dic["outputParam"] = ((Newtonsoft.Json.Linq.JArray)dic["outputParam"]).ToObject<string[]>();
                result = bll.Add(dic);
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 获取API列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage List([FromBody]string condition)
        {
            ReturnData result = new ReturnData();
            try
            {
                DataSet ds = bll.GetList(condition);
                result.D = ds.Tables[0];
                result.S = true;
                //result.D = colBLL.GetList(tableID).Tables[0];
            }
            catch (Exception ex)
            {
                result.S = false;
                result.SetErrorMsg("获取表字段失败：" + ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// 测试是否能够调通tushare接口
        /// </summary>
        /// <param name="postData">接口地址和json数据</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Test(JObject postData)
        {
            ReturnData result = new ReturnData();
            try
            {
                HttpHelper hh = new HttpHelper();
                result.S = true;
                result.D = hh.Post(postData.GetValue("url").ToString(), postData.GetValue("postData").ToJson());
            }
            catch (Exception ex)
            {

                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        /// <summary>
        /// Get方式获取url的Html
        /// </summary>
        /// <param name="url">指定页面url</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetHtml([FromBody]string url)
        {
            ReturnData result = new ReturnData();
            try
            {
                HttpHelper hh = new HttpHelper();
                result.D = hh.Get(url, "");
                result.S = true;
            }
            catch (Exception ex)
            {
                result.SetErrorMsg(ex.Message);
            }
            return MyResponseMessage.SuccessJson(result);
        }
        #endregion


        #region Http Request
        /// <summary>
        /// 请求Tushare获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public TuShareResult Post(string url, string body)
        {
            HttpHelper hh = new HttpHelper();
            string result = HttpUtility.UrlDecode(hh.Post(url, body));          
            TuShareResult apiResult = JSONUtil.ParseFromJson<TuShareResult>(result);
            if (apiResult.code == "40203")
            {
                Common.LogHelper.Default.WriteError("111111");
                Common.LogHelper.Default.WriteError("Result:" + result);
                apiResult = Post(url, body);
            }
            else
            {
                Common.LogHelper.Default.WriteError("222222:" + body);
            }
            return apiResult;
        }
        /// <summary>
        /// 请求Tushare获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="threadID"></param>
        /// <returns></returns>
        public TuShareResult MultiPost(string url, string body,string threadID)
        {
            HttpHelper hh = new HttpHelper();
            string result = HttpUtility.UrlDecode(hh.Post(url, body));
            TuShareResult apiResult = JSONUtil.ParseFromJson<TuShareResult>(result);
            if (apiResult.code == "40203")
            {
                Common.LogHelper.Default.WriteError("Thread:" + threadID);
                Common.LogHelper.Default.WriteError("Fail:"+ result);
                apiResult = Post(url, body);
            }
            else
            {
                Common.LogHelper.Default.WriteError("Thread:" + threadID);
                Common.LogHelper.Default.WriteError("Success:" + result);
            }
            return apiResult;
        }
        #endregion
    }
}
