using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UFIDA.U9.CBO.PubBE.YYC;

namespace ConsoleTest
{
    public class ESignUtils
    {
        string testlHost = "https://smlopenapi.esign.cn";
        string normalHost = "https://openapi.esign.cn";
        string secret = "e2d453c0af8eee24a859292f8bf4a50a";
        string appID = "7438973960";
        public string GetToken()
        {
            string token = "";
            string oauthPath = "/v1/oauth2/access_token?appId=" + appID + "&secret=" + secret + "&grantType=client_credentials";
            string uri = testlHost + oauthPath;
            HttpHelper http = new HttpHelper();
            //http.CreateRequest(testlHost + oauthPath, "", "GET");
            string result = http.Get(uri, "");
            EsignResult<TokenData> tokenInfo = JsonHelper.JsonDeserializeJS<EsignResult<TokenData>>(result);
            TokenData data = CheckEsignResult<TokenData>(tokenInfo);
            token = data.token;
            return token;
        }
        public void GetFileUrl(string signFlow)
        {
            string token = GetToken();
            string path = string.Format("/v3/sign-flow/{0}/file-download-url", signFlow);
            string uri = testlHost + path;
            HttpHelper http = new HttpHelper();
            HttpWebRequest request = http.CreateRequest(uri, "", "GET");
            request.Headers.Add("X-Tsign-Open-App-Id:" + appID);
            request.Headers.Add("X-Tsign-Open-Token:" + token);
            //request.Headers.Add("Content-Type:application/json; charset=UTF-8");
            request.ContentType = "application/json; charset=UTF-8";
            string result = http.GetResponse(request);
            EsignResult<FileInfo> fileUrlInfo = JsonHelper.JsonDeserializeJS<EsignResult<FileInfo>>(result);
            FileInfo fileInfo = CheckEsignResult<FileInfo>(fileUrlInfo);

            List< Files> files = fileInfo.files;
            if (files!=null&&files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    string fileId = files[i].fileId;
                    string fileName = files[i].fileName;
                    string downloadUrl = files[i].downloadUrl;
                    //TODO:文件上传U9文档库
                    GetFile(downloadUrl);
                }
            }
            List< Attachments> attachments = fileInfo.attachments;
            if (attachments != null && attachments.Count > 0)
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    string fileId = attachments[i].fileId;
                    string fileName = attachments[i].fileName;
                    string downloadUrl = attachments[i].downloadUrl;
                    //TODO:文件上传U9文档库
                    GetFile(downloadUrl);
                }
            }
        }
        public string InsertFileInfo(byte[] upBytes)
        {
            SqlConnection sqlCon2 = new SqlConnection("User Id=sa;Password=auctus@168;Data Source=192.168.1.82;Initial Catalog=AuctusERPD;packet size=4096;Max Pool size=500;Connection Timeout=15;persist security info=True;MultipleActiveResultSets=true;");
            string sqlIns = @"INSERT INTO dbo.FileInfo
        ( ID, FileName, Content, Compress )
VALUES  ( @ID, -- ID - nvarchar(50)
          @FileName, -- FileName - nvarchar(200)
          @Content, -- Content - varbinary(max)
          @Compress  -- Compress - bit
          )";
            SqlCommand cmd2 = new SqlCommand(sqlIns, sqlCon2);
            SqlParameter[] pars = { new SqlParameter("@ID", SqlDbType.NVarChar), new SqlParameter("@FileName", SqlDbType.NVarChar), new SqlParameter("@Content", SqlDbType.VarBinary), new SqlParameter("@Compress", SqlDbType.Bit) };
            string guid = Guid.NewGuid().ToString();
            pars[0].Value = guid;
            pars[1].Value = "test" + DateTime.Now.ToString() + ".pdf";
            pars[2].Value = upBytes;
            pars[3].Value = false;
            cmd2.Parameters.AddRange(pars);
            sqlCon2.Open();
            cmd2.ExecuteNonQuery();
            sqlCon2.Close();
            return guid;
        }

        private T CheckEsignResult<T>(EsignResult<T> esignInfo) where T : class
        {
            if (esignInfo.code != 0)//访问不成功
            {
                throw new Exception("获取e签宝token失败，业务码：" + esignInfo.code.ToString() + "，业务信息：" + esignInfo.message);
            }
            return esignInfo.data;
        }
        /// <summary>
        /// 获取文件字节流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public byte[] GetFile(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse res = request.GetResponse() as HttpWebResponse;
            Stream sr = (Stream)res.GetResponseStream();
            StreamReader sr2 = new StreamReader(sr);
            MemoryStream ms = new MemoryStream();
            byte[] buffer=new byte[1024];
            int size = 0;
            int bLen = 1024;
            size = sr.Read(buffer, 0, bLen);
            while (size > 0)
            {
                ms.Write(buffer, 0, size);
                size=sr.Read(buffer, 0, bLen);
            }
            return ms.ToArray();
        }
    }
    public class EsignResult<T> where T : class
    {
        //public EsignResult<T>(){}
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
    public class TokenData
    {
        public string token { get; set; }
        public string expiresIn { get; set; }
        public string refreshToken { get; set; }
    }
    public class FileInfo
    {
        public List<Files> files { get; set; }
        public List<Attachments> attachments { get; set; }
    }
    public class Files
    {
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string downloadUrl { get; set; }
    }
    public class Attachments
    {
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string downloadUrl { get; set; }
    }
}
