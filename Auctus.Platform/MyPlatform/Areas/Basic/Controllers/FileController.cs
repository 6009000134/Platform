using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyPlatform.Areas.Basic.Controllers
{
    /// <summary>
    /// 文件上传服务
    /// </summary>
    public class FileController : ApiController
    {
        private string basePath = AppContext.BaseDirectory;
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post()
        {
            ReturnData result = new ReturnData();
            try
            {
                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                if (files.Count > 0)
                {
                    foreach (string key in files.AllKeys)
                    {
                        HttpPostedFile file = files[key];
                        UploadFile(file);
                    }
                }
                else
                {
                    result.SetErrorMsg("上传文件数量为0，上传失败！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        public void UploadFile(HttpPostedFile file)
        {
            string FileEextension = Path.GetExtension(file.FileName);
            //设置文件上传路径
            string path = AppContext.BaseDirectory + "UpLoad/" + FileEextension.Substring(1) + "/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString();
            ////创建文件夹，保存文件
            //string path = Path.GetDirectoryName(fullFileName);
            //检查上传的物理路径是否存在，不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + FileEextension;
            string fullFileName = path + "/" + newFileName;
            int i = 0;
            while (File.Exists(fullFileName))
            {
                fullFileName = path + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "(" + i.ToString() + ")" + FileEextension;
            }
            //将文件原名和重命名文件保存到数据库
            //FileInfoBLL
            //string sql="Insert into sys_fileInfo "
            file.SaveAs(fullFileName);
        }
        /// <summary>
        /// 获取指定目录下文件列表
        /// </summary>
        /// <param name="basicpath"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage GetListByPath([FromBody]string basicpath)
        {
            ReturnData result = new ReturnData();
            try
            {
                DirectoryHelper dirHelper = new DirectoryHelper();
                //List<string> dirs= dirHelper.GetAllDirectories(basicpath);
                List<string> allFiles = dirHelper.GetAllFiles(basicpath);
                for (int i = 0; i < allFiles.Count; i++)
                {
                    allFiles[i]=allFiles[i].Replace(basicpath, "@").Replace("\\","/");
                }
                result.D = allFiles;
                result.S = true;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MyResponseMessage.SuccessJson<ReturnData>(result);
        }
    }
}
