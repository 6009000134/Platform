using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.SQLServerDAL
{

    public static class DALUtils
    {
        /// <summary>
        /// 计算开始页码
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int CalStartIndex(Pagination page)
        {
            return page.PageSize * (page.PageIndex - 1);
        }
        /// <summary>
        /// 计算结尾页码
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static int CalEndIndex(Pagination page)
        {
            return page.PageSize * page.PageIndex + 1;
        }
        /// <summary>
        /// 计算开始页码
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static int CalStartIndex(int pageSize, int pageIndex)
        {
            return pageSize * (pageIndex - 1);
        }
        /// <summary>
        /// 计算结尾页码
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static int CalEndIndex(int pageSize, int pageIndex)
        {
            return pageSize * pageIndex + 1;
        }
    }
}
