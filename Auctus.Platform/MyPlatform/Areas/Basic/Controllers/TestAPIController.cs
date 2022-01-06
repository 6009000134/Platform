using MyPlatform.Model;
using MyPlatform.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 学习时编写测试代码
    /// </summary>
    public class TestAPIController : ApiController
    {        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostFile()
        {
            ReturnData result = new ReturnData();
            try
            {
                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count > 0)
                {
                    //得到上传文件格式
                    string FileEextension = Path.GetExtension(files[0].FileName);
                    //设置文件上传路径
                    string fullFileName = AppContext.BaseDirectory + "UpLoad/" + FileEextension.Substring(1) +"/"+ Path.GetFileName(files[0].FileName);
                    ////创建文件夹，保存文件
                    string path = Path.GetDirectoryName(fullFileName);
                    #region 检查上传的物理路径是否存在，不存在则创建
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    #endregion
                    //保存文件  文件存在则先删除原来的文件
                    if (File.Exists(fullFileName))
                    {
                        File.Delete(fullFileName);
                    }
                    files[0].SaveAs(fullFileName);
                }
                var fileProvider = new MultipartMemoryStreamProvider();
                Request.Content.ReadAsMultipartAsync(fileProvider);
                foreach (var item in fileProvider.Contents)
                {
                    // 判断是否是文件  
                    if (item.Headers.ContentDisposition.FileName == null) continue;
                    // 获取到流  
                    var ms = item.ReadAsStreamAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        [HttpPost]
        public HttpResponseMessage List(List<QueryParamModel> qd)
        {
            ReturnData result = new ReturnData();
            result.S = true;
            result.D = qd;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
        [HttpPost]
        public HttpResponseMessage List2(JObject obj)
        {
            List<QueryParamModel> qd = obj["qd"].ToObject<List<QueryParamModel>>();
            string name = obj["name"].ToString();
            ReturnData result = new ReturnData();
            result.S = true;
            //result.D = qd;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        public HttpResponseMessage List3(JObject obj)
        {
            ReturnData result = new ReturnData();
            List<QueryParamModel> qd = obj["qd"].ToObject<List<QueryParamModel>>();
            List<QueryParamModel> qd2 = obj["qd2"].ToObject<List<QueryParamModel>>();
            result.S = true;
            result.D = obj;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        public HttpResponseMessage List4()
        {
            ReturnData result = new ReturnData();
            result.S = true;
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
    }
}
