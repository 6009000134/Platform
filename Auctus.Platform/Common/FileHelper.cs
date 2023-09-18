using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 路径是否存在
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public bool IsExistDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileContent">文件byte数组</param>
        /// <param name="path">路径（路径需以\\结尾）</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public bool SaveFile(byte[] fileContent, string path, string fileName)
        {
            if (!path.EndsWith("\\"))
            {
                throw new Exception("");
            }
            //检测路径是否存在，不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return SaveFile(fileContent,path + fileName);
        }
        public bool SaveFile(byte[] fileContent,string filePath)
        {
            try
            {
                if (fileContent.Length==0)
                {
                    throw new Exception("文件的字节流为空，无效文件！");
                }
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(fileContent, 0, fileContent.Length);
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
