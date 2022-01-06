using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Model.Chart
{
    /// <summary>
    /// Echart图表实体
    /// </summary>
    public class EChartModel
    {
        public EChartModel()
        {
            title = new List<Dictionary<string,object>>();
            legend = new List<Dictionary<string, object>>();
            xAxis = new List<Dictionary<string, object>>();
            yAxis = new List<Dictionary<string, object>>();
            grid = new List<Dictionary<string, object>>();
            series = new List<Dictionary<string, object>>();
            dataset = new List<Dictionary<string, object>>();
            tooltip = new Dictionary<string, object>();
        }
        /// <summary>
        /// 前端DOM ID
        /// </summary>
        public string DomID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public List<Dictionary<string, object>> title { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        public Dictionary<string, object> tooltip { get; set; }
        /// <summary>
        /// 图例
        /// </summary>
        public List<Dictionary<string, object>> legend { get; set; }
        /// <summary>
        /// x轴
        /// </summary>
        public List<Dictionary<string, object>> xAxis { get; set; }
        /// <summary>
        /// y轴
        /// </summary>
        public List<Dictionary<string, object>> yAxis { get; set; }
        /// <summary>
        /// grid
        /// </summary>
        public List<Dictionary<string, object>> grid { get; set; }
        /// <summary>
        /// 系列
        /// </summary>
        public List<Dictionary<string, object>> series { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        public List<Dictionary<string, object>> dataset { get; set; }
    }
}
