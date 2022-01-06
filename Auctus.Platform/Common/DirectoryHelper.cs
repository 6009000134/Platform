using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyPlatform.Common
{
    public class DirectoryHelper
    {
        public List<string> GetAllFiles(string path)
        {
            List<string> paths = GetAllDirectories(path);
            List<string> result = new List<string>();
            if (paths.Count>0)
            {
                for (int i = 0; i < paths.Count; i++)
                {
                    result.AddRange(Directory.GetFiles(paths[i]));
                }
            }
            return result;
        }
        /// <summary>
        /// 获取path下所有路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetAllDirectories(string path)
        {
            List<string> result = new List<string>();
            result=GetDirs(path, result);
            return result;
            //if (subDirs.Count == 0)
            //{
            //    return result;
            //}
            //else
            //{
            //    for (int i = 0; i < subDirs.Count; i++)
            //    {
            //        result.Add(subDirs[i]);
            //        GetAllDirectories(subDirs[i]);
            //    }
            //    return result;
            //}
        }
        public List<string> GetDirs(string path,List<string> paths)
        {
            List<string> subDirs = GetSubDirectory(path);
            if (subDirs.Count > 0)
            {
                for (int i = 0; i < subDirs.Count; i++)
                {
                    paths.Add(subDirs[i]);
                    GetDirs(subDirs[i], paths);
                }
            }            
            return paths;
        }
        /// <summary>
        /// 获取下级子目录
        /// </summary>
        /// <param name="path">目标路径</param>
        /// <returns></returns>
        public List<string> GetSubDirectory(string path)
        {
            List<string> subDirs = Directory.GetDirectories(path).ToList();
            return subDirs;
        }


    }
}
