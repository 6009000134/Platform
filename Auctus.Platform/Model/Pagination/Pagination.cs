using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Pagination()
        {

        }
        private int _pageSize;
        /// <summary>
        /// 页尺寸
        /// </summary>
        public int pageSize
        {
            get { return _pageSize; }
            set
            {
                //pageSize = value;
                _pageSize = value;
                if (_pageIndex > 0)
                {
                    startIndex = pageSize * (pageIndex - 1);
                    endIndex = pageSize * pageIndex + 1;
                }
            }
        }
        private int _pageIndex;
        /// <summary>
        /// 页码
        /// </summary>
        public int pageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                if (_pageSize > 0)
                {
                    startIndex = pageSize * (pageIndex - 1);
                    endIndex = pageSize * pageIndex + 1;
                }

            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; }
        /// <summary>
        /// 起始Index
        /// </summary>
        public int startIndex { get; set; }
        /// <summary>
        /// 结尾Index
        /// </summary>
        public int endIndex { get; set; }
        /// <summary>
        /// 计算页码
        /// </summary>
        public void CalPageNumber()
        {
            startIndex = pageSize * (pageIndex - 1);
            endIndex = pageSize * pageIndex + 1;
        }
    }
}
