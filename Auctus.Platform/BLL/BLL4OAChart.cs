using MyPlatform.DBUtility;
using MyPlatform.Model;
using MyPlatform.Model.Chart;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class BLL4OAChart
    {

        public ReturnData GetData(string xmbm, string strTypes)
        {
            ReturnData result = new ReturnData();
            IDataBase db = DBHelperFactory.Create("OACon");
            string[] arr = strTypes.Split(',');
            List<EChartModel> list = new List<EChartModel>();
            for (int i = 0; i < arr.Length; i++)
            {
                switch (arr[i])
                {
                    //case "Bug状态":                        
                    //case "发生阶段":
                    //case "故障大类":
                    //case "故障小类":
                    //case "严重程度":
                    case "1":
                    case "2":
                    case "3":
                    case "5":
                        list.Add(GetEchart1(db, xmbm, arr[i], "1"));
                        break;
                    case "4":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "10":
                        list.Add(GetEchart2(db, xmbm, arr[i], "1"));
                        break;
                    case "11":
                    case "12":
                    case "13":
                    case "14":
                    case "15":
                        list.Add(GetEchart3(db, xmbm, arr[i], "1"));
                        break;
                    case "16":
                    case "17":
                    case "18":
                    case "19":
                        list.Add(GetEchart4(db, xmbm, arr[i], "1"));
                        break;
                    default:
                        break;
                }
            }
            result.D = list;
            return result;
        }
        #region 分布图
        public EChartModel GetEchart3(IDataBase db, string xmbm, string type, string sfsx)
        {
            EChartModel model = new EChartModel();
            string domid = "main" + type;
            model.DomID = domid;
            Dictionary<string, object> title = new Dictionary<string, object>();
            Dictionary<string, object> dicEncode = new Dictionary<string, object>();
            Dictionary<string, object> label = new Dictionary<string, object>();
            //legend设置
            Dictionary<string, object> legend = new Dictionary<string, object>();
            Dictionary<string, object> legendSelected = new Dictionary<string, object>();
            //Grid设置
            model.grid.Add(new Dictionary<string, object>());
            //标题设置
            string titleText = "";
            string sql = "";
            switch (type)
            {
                case "11":
                    titleText = "Bug状态下的严重等级";
                    model.grid[0].Add("top", "20%");
                    sql = string.Format(@"select 
a.xmbm,a.xmbm1,a.xmmc,a.item1,a.item2,b.quantity
from 
(
select a.xmbm,a.xmbm1,a.xmmc,a.item item1,b.item item2 from (select xmbm,xmbm1,xmmc,item from v_bugfree_items where type='1')a,
(select xmbm,item from v_bugfree_items where type='5') b where a.xmbm=b.xmbm and a.xmbm='{0}'
) a
left join 
(select a.xmmc,a.xmbm,a.status item1,a.yccd,a.yccdmc item2,count(1)Quantity from v_bugfreeinfo a 
where  a.xmbm='{0}'
group by a.xmmc,a.xmbm,a.status,a.yccd,a.yccdmc) b on a.xmbm=b.xmbm and a.item1=b.item1 and a.item2=b.item2 ", xmbm);
                    break;
                case "12":
                    model.grid[0].Add("top", "20%");
                    titleText = "发生阶段下严重等级";
                    sql = string.Format(@"select 
a.xmbm,a.xmbm1,a.xmmc,a.item1,a.item2,b.quantity
from 
(
select a.xmbm,a.xmbm1,a.xmmc,a.item item1,b.item item2 from (select xmbm,xmbm1,xmmc,item from v_bugfree_items where type='2')a,
(select xmbm,item from v_bugfree_items where type='5') b where a.xmbm=b.xmbm and a.xmbm='{0}'
) a
left join 
(select a.xmmc,a.xmbm,a.wtfxjd item1,a.yccd,a.yccdmc item2,count(1)Quantity from v_bugfreeinfo a 
group by a.xmmc,a.xmbm,a.wtfxjd,a.yccd,a.yccdmc) b on a.xmbm=b.xmbm and a.item1=b.item1 and a.item2=b.item2
", xmbm);
                    break;
                case "13":
                    model.grid[0].Add("top", "20%");
                    titleText = "故障大类下故障小类";
                    sql = string.Format(@"select 
a.xmbm,a.xmbm1,a.xmmc,a.item1,a.item2,b.quantity
from 
(
select a.xmbm,a.xmbm1,a.xmmc,b.iName item1,b.iiname item2 from (select distinct xmbm,xmmc,xmbm1 from v_bugfreeinfo where nvl(xmbm,'-1')!='-1')a,
(select IName,IIName from auctus_fault)b
where a.xmbm='{0}'
) a
left join 
(select a.xmmc,a.xmbm,a.parenttype item1,a.childtype item2,count(1)Quantity from v_bugfreeinfo a 
group by a.xmmc,a.xmbm,a.flid,a.sjfl,a.parenttype,a.childtype) b on a.xmbm=b.xmbm and a.item1=b.item1 and a.item2=b.item2
 ", xmbm);
                    break;
                case "14":
                    model.grid[0].Add("top", "20%");
                    titleText = "故障大类下的严重等级";
                    sql = string.Format(@"select 
a.xmbm,a.xmmc,a.item1,a.item2,b.quantity
from 
(
select a.xmbm,a.xmmc,a.item item1,b.item item2 from (select xmbm,xmmc,item from v_bugfree_items where type='3')a,
(select xmbm,item from v_bugfree_items where type='5') b where a.xmbm=b.xmbm and a.xmbm='{0}'
) a
left join 
(select a.xmmc,a.xmbm,a.parenttype item1,a.yccd,a.yccdmc item2,count(1)Quantity from v_bugfreeinfo a 
group by a.xmmc,a.xmbm,a.parenttype,a.yccd,a.yccdmc) b on a.xmbm=b.xmbm and a.item1=b.item1 and a.item2=b.item2", xmbm);
                    break;
                case "15":
                    model.grid[0].Add("top", "20%");
                    titleText = "发生阶段下的问题责任分类";
                    sql = string.Format(@"select 
a.xmbm,a.xmmc,a.item1,a.item2,b.quantity
from 
(
select a.xmbm,a.xmmc,a.item item1,b.item item2 from (select xmbm,xmmc,item from v_bugfree_items where type='2')a,
(select xmbm,item from v_bugfree_items where type='10') b where a.xmbm=b.xmbm and a.xmbm='{0}'
) a
left join 
(select a.xmmc,a.xmbm,a.wtfxjd item1,a.wtzrfl,a.wtlymc item2,count(1)Quantity from v_bugfreeinfo a 
group by a.xmmc,a.xmbm,a.wtfxjd,a.wtzrfl,a.wtlymc) b on a.xmbm=b.xmbm and a.item1=b.item1 and a.item2=b.item2", xmbm);
                    break;
                default:
                    break;
            }
            if (sfsx == "0")
            {
                sql += " sfsx=0";
            }
            title.Add("text", titleText);
            model.title.Add(title);

            //series设置
            DataSet ds = db.Query(sql);
            DataView dv = ds.Tables[0].DefaultView;
            DataTable dtItem1 = dv.ToTable(true, new string[] { "Item1" });
            DataTable dtItem2 = dv.ToTable(true, new string[] { "Item2" });
            List<List<string>> source = new List<List<string>>();
            if (dtItem1.Rows.Count > 0)
            {
                string[] arrTitle = new string[dtItem1.Rows.Count + 1];
                List<string> temp = new List<string>();
                temp.Add("table");
                for (int i = 0; i < dtItem1.Rows.Count; i++)
                {
                    temp.Add(dtItem1.Rows[i][0].ToString());
                }
                source.Add(temp);
            }
            if (dtItem2.Rows.Count > 0)
            {
                for (int i = 0; i < dtItem2.Rows.Count; i++)
                {
                    List<string> temp = new List<string>();
                    temp.Add(dtItem2.Rows[i][0].ToString());
                    for (int j = 0; j < dtItem1.Rows.Count; j++)
                    {
                        DataRow[] drs = ds.Tables[0].Select("Item2='" + dtItem2.Rows[i][0].ToString() + "' and item1='" + dtItem1.Rows[j][0].ToString() + "'");
                        if (drs.Count() > 0)
                        {
                            temp.Add(drs[0]["Quantity"].ToString());
                        }
                        else
                        {
                            temp.Add("0");
                        }
                    }
                    source.Add(temp);
                    //系列设置
                    Dictionary<string, object> series = new Dictionary<string, object>();
                    series.Add("type", "bar");
                    //label.Add("show", "true");
                    //label.Add("formatter", "{a}({@" + i.ToString() + "})");
                    //label.Add("fontSize", "14px");
                    //series.Add("label", label);
                    series.Add("seriesLayoutBy", "row");
                    model.series.Add(series);
                }
            }
            Dictionary<string, object> dicSource = new Dictionary<string, object>();
            dicSource.Add("source", source);
            model.dataset.Add(dicSource);
            legend.Add("top", "8%");
            //x轴设置
            Dictionary<string, object> xAxis = new Dictionary<string, object>();
            xAxis.Add("type", "category");
            Dictionary<string, object> splitLine = new Dictionary<string, object>();
            splitLine.Add("show",true);
            xAxis.Add("splitLine",splitLine);
            model.xAxis.Add(xAxis);
            //y轴设置
            Dictionary<string, object> yAxis = new Dictionary<string, object>();
            model.yAxis.Add(yAxis);
            //legend  
            model.legend.Add(legend);
            return model;
        }
        #endregion
        #region 趋势图
        public EChartModel GetEchart4(IDataBase db, string xmbm, string type, string sfsx)
        {
            EChartModel model = new EChartModel();
            string domid = "main" + type;
            model.DomID = domid;
            Dictionary<string, object> title = new Dictionary<string, object>();
            Dictionary<string, object> dicEncode = new Dictionary<string, object>();
            Dictionary<string, object> label = new Dictionary<string, object>();
            //legend设置
            Dictionary<string, object> legend = new Dictionary<string, object>();
            Dictionary<string, object> legendSelected = new Dictionary<string, object>();
            //Grid设置
            model.grid.Add(new Dictionary<string, object>());
            //标题设置
            string titleText = "";
            string sql = "";
            switch (type)
            {
                case "16":
                    titleText = "故障大类";
                    model.grid[0].Add("top", "20%");
                    sql = string.Format(@"select a.xmmc,a.xmbm,a.item,a.sfsx,b.sqrq,b.quantity
from v_bugfree_items a left join 
(
select a.xmbm,a.xmmc,substr(a.sqrq,0,7)sqrq,a.sjfl ItemID,a.ParentType Item,count(1)Quantity
from v_bugfreeinfo a
where nvl(a.xmbm,'-1')!='-1'
and a.nodeorder>0 and a.xmbm='{0}'
group by a.xmbm,a.xmmc,substr(a.sqrq,0,7),a.sjfl,a.parenttype
)b on a.xmbm=b.xmbm and a.item=b.item
where a.xmbm='{0}' and a.type='3'
order by a.item,b.sqrq ", xmbm);
                    break;
                case "17":
                    model.grid[0].Add("top", "20%");
                    titleText = "严重等级";
                    sql = string.Format(@"select a.xmmc,a.xmbm,a.item,a.sfsx,b.sqrq,b.quantity
from v_bugfree_items a left join 
(
select a.xmbm,a.xmmc,substr(a.sqrq,0,7)sqrq,a.yccd ItemID,a.yccdmc Item,count(1)Quantity
from v_bugfreeinfo a
where nvl(a.xmbm,'-1')!='-1'
and a.nodeorder>0 and a.xmbm='{0}'
group by a.xmbm,a.xmmc,substr(a.sqrq,0,7),a.yccd,a.yccdmc
)b on a.xmbm=b.xmbm and a.item=b.item
where a.xmbm='{0}' and a.type='5'
order by a.item,b.sqrq
", xmbm);
                    break;
                case "18":
                    model.grid[0].Add("top", "20%");
                    titleText = "责任分类";
                    sql = string.Format(@"select a.xmmc,a.xmbm,a.item,a.sfsx,b.sqrq,b.quantity
from v_bugfree_items a left join 
(
select a.xmbm,a.xmmc,substr(a.sqrq,0,7)sqrq,a.wtzrfl ItemID,a.wtlymc Item,count(1)Quantity
from v_bugfreeinfo a
where nvl(a.xmbm,'-1')!='-1'
and nvl(a.wtlymc,'-1')!='-1'
and a.nodeorder>0 and a.xmbm='{0}'
group by a.xmbm,a.xmmc,substr(a.sqrq,0,7),a.wtlymc,a.wtzrfl
)b on a.xmbm=b.xmbm and a.item=b.item
where a.xmbm='{0}' and a.type='10'
order by a.item,b.sqrq ", xmbm);
                    break;
                case "19":
                    model.grid[0].Add("top", "20%");
                    titleText = "超时数";
                    sql = string.Format(@"select a.xmbm,a.xmmc,'超时'item,0 sfsx,substr(a.sqrq,0,7)sqrq,count(1)Quantity
from v_bugfreeinfo a  
where  nvl(a.xmbm,'-1')!='-1'
and a.nodeorder>0 and a.xmbm='{0}'
and to_char(nvl(a.actualcompletedate,sysdate),'yyyy-MM-dd')>a.yqwcrq
group by a.xmbm,a.xmmc,substr(a.sqrq,0,7)
order by sqrq", xmbm);
                    break;
                default:
                    break;
            }
            if (sfsx == "0")
            {
                sql += " sfsx=0";
            }
            title.Add("text", titleText);
            model.title.Add(title);

            //series设置
            DataSet ds = db.Query(sql);
            DataView dv = ds.Tables[0].DefaultView;
            DataTable dtLegend = dv.ToTable(true, new string[] { "Item" });
            dv.Sort = " sqrq ";
            dv.RowFilter = " sqrq<>''";
            DataTable dtDate = dv.ToTable(true, new string[] { "SQRQ" });
            //legend data设置
            if (dtLegend.Rows.Count > 0)
            {
                string[] arr = new string[dtLegend.Rows.Count];

                for (int i = 0; i < dtLegend.Rows.Count; i++)
                {
                    arr[i] = dtLegend.Rows[i][0].ToString();
                }
                legend.Add("data", arr);
            }
            for (int i = 0; i < dtLegend.Rows.Count; i++)
            {
                label = new Dictionary<string, object>();
                Dictionary<string, object> series = new Dictionary<string, object>();
                series.Add("name", dtLegend.Rows[i][0]);
                List<int> liSData = new List<int>();
                DataRow[] drs = ds.Tables[0].Select("Item='" + dtLegend.Rows[i][0].ToString() + "'");
                for (int j = 0; j < dtDate.Rows.Count; j++)
                {
                    DataRow[] drSelect = ds.Tables[0].Select("Item='" + dtLegend.Rows[i][0].ToString() + "' and sqrq='" + dtDate.Rows[j][0].ToString() + "'");
                    if (drSelect.Count() > 0)
                    {
                        liSData.Add(Convert.ToInt32(drSelect[0]["Quantity"]));
                    }
                    else
                    {
                        liSData.Add(0);
                    }
                }
                series.Add("data", liSData);
                //系列设置
                series.Add("type", "line");
                label.Add("show", "true");
                //label.Add("formatter", "{a}({@" + i.ToString() + "})");
                label.Add("fontSize", "14px");
                series.Add("label", label);
                //series.Add("seriesLayoutBy", "row");
                model.series.Add(series);
            }


            //legend.Add("selected", legendSelected);
            legend.Add("top", "8%");
            //Dictionary<string, object> dicTemp = new Dictionary<string, object>();
            //dicTemp.Add("source", liData);
            //model.dataset.Add(dicTemp);
            //x轴设置
            Dictionary<string, object> xAxis = new Dictionary<string, object>();
            if (dtDate.Rows.Count > 0)
            {
                string[] arrDate = new string[dtDate.Rows.Count];
                for (int i = 0; i < dtDate.Rows.Count; i++)
                {
                    arrDate[i] = dtDate.Rows[i][0].ToString();
                }
                xAxis.Add("data", arrDate);
            }
            xAxis.Add("type", "category");
            xAxis.Add("boundaryGap", false);
            //xAxis.Add("data",arr);
            //Dictionary<string, object> axisLabel = new Dictionary<string, object>();
            //axisLabel.Add("show", false);
            //xAxis.Add("axisLabel", axisLabel);
            model.xAxis.Add(xAxis);
            //y轴设置
            Dictionary<string, object> yAxis = new Dictionary<string, object>();
            //yAxis.Add("gridIndex", 0);
            model.yAxis.Add(yAxis);
            //legend  
            model.legend.Add(legend);
            return model;
        }
        #endregion
        #region 排序图表
        public EChartModel GetEchart2(IDataBase db, string xmbm, string type, string sfsx)
        {
            EChartModel model = new EChartModel();
            string domid = "main" + type;
            model.DomID = domid;
            Dictionary<string, object> title = new Dictionary<string, object>();
            Dictionary<string, object> dicEncode = new Dictionary<string, object>();
            Dictionary<string, object> label = new Dictionary<string, object>();
            //legend设置
            Dictionary<string, object> legend = new Dictionary<string, object>();
            Dictionary<string, object> legendSelected = new Dictionary<string, object>();
            //Grid设置
            model.grid.Add(new Dictionary<string, object>());
            //标题设置
            string titleText = "";
            string sql = "";
            switch (type)
            {
                case "6":
                    titleText = "问题发现阶段";
                    type = "2";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmmc, a.xmbm, a.wtfxjd, count(a.wtfxjd)Quantity, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a
where a.nodeorder > 0
                                                                                   group by a.xmmc, a.xmbm, a.wtfxjd
) b on a.xmbm = b.xmbm and a.type = b.type  and a.item=b.wtfxjd
where a.type = '{1}'and a.xmbm = '{2}'
order by Quantity desc", titleText, type, xmbm);
                    break;
                case "7":
                    titleText = "故障大类";
                    type = "3";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmbm,a.xmmc,count(a.sjfl)Quantity,a.parenttype, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.sjfl,a.parenttype
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.parenttype
where a.type = '{1}'and a.xmbm = '{2}'
order by Quantity desc", titleText, type, xmbm);
                    break;
                case "4":
                case "8":
                    model.grid[0].Add("top", "40%");
                    titleText = "故障小类";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmbm,a.xmmc,count(a.flid)Quantity,a.childtype, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.flid,a.childtype
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.childtype
where a.type = '{1}'and a.xmbm = '{2}'", titleText, "4", xmbm);
                    if (type == "8")
                    {
                        sql += " order by Quantity desc";
                    }
                    break;
                case "9":
                    titleText = "严重程度";
                    type = "5";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmbm,a.xmmc,count(a.yccdmc)Quantity,a.yccdmc,'{1}'Type, '{0}'TypeName from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.yccdmc
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.yccdmc
where a.type = '{1}'and a.xmbm = '{2}'
order by Quantity desc", titleText, type, xmbm);
                    break;
                case "10":
                    titleText = "问题责任分类";
                    sql = string.Format(@"select '{0}'Item,'数量' Quantity from dual
union all
select a.item,to_char(nvl(b.Quantity,0))Quantity from v_bugfree_items a 
left join 
(
select a.xmbm,a.xmmc,count(a.wtzrfl)Quantity,a.wtlymc,'{1}'Type,'{0}'TypeName 
from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.wtzrfl,a.wtlymc
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.wtlymc
where a.type='{1}' and a.xmbm='{2}'
order by quantity desc", titleText, type, xmbm);
                    break;
                default:
                    break;
            }
            if (sfsx == "0")
            {
                sql += " sfsx=0";
            }
            title.Add("text", titleText);
            model.title.Add(title);

            //dataset设置
            DataSet ds = db.Query(sql);
            List<List<string>> liData = new List<List<string>>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<string> temp = new List<string>();
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    temp.Add(ds.Tables[0].Rows[i][j].ToString());
                }
                liData.Add(temp);
                if (i > 0)
                {
                    Dictionary<string, object> series = new Dictionary<string, object>();
                    label = new Dictionary<string, object>();
                    //系列设置
                    series.Add("type", "bar");
                    label.Add("show", "true");
                    label.Add("formatter", "{a}({@" + i.ToString() + "})");
                    label.Add("fontSize", "14px");
                    series.Add("label", label);
                    //dicEncode.Add("itemName", titleText);
                    //dicEncode.Add("value", "数量");
                    //series.Add("encode", dicEncode);
                    series.Add("seriesLayoutBy", "row");
                    model.series.Add(series);
                    //设置没数据的选不被选中
                    if (type == "4" || type == "8")
                    {
                        if (ds.Tables[0].Rows[i][1].ToString() == "0")
                        {
                            if (!legendSelected.ContainsKey(ds.Tables[0].Rows[i][0].ToString()))
                            {
                                legendSelected.Add(ds.Tables[0].Rows[i][0].ToString(), false);
                            }
                        }
                    }
                }
            }
            legend.Add("selected", legendSelected);
            legend.Add("top", "8%");
            Dictionary<string, object> dicTemp = new Dictionary<string, object>();
            dicTemp.Add("source", liData);
            model.dataset.Add(dicTemp);
            //x轴设置
            Dictionary<string, object> xAxis = new Dictionary<string, object>();
            xAxis.Add("type", "category");
            xAxis.Add("gridIndex", 0);
            Dictionary<string, object> axisLabel = new Dictionary<string, object>();
            axisLabel.Add("show", false);
            xAxis.Add("axisLabel", axisLabel);
            model.xAxis.Add(xAxis);
            //y轴设置
            Dictionary<string, object> yAxis = new Dictionary<string, object>();
            yAxis.Add("gridIndex", 0);
            model.yAxis.Add(yAxis);
            //legend  
            model.legend.Add(legend);
            return model;
        }
        #endregion
        #region 数量图表
        public EChartModel GetEchart1(IDataBase db, string xmbm, string type, string sfsx)
        {
            EChartModel model = new EChartModel();
            string domid = "main" + type;
            model.DomID = domid;
            Dictionary<string, object> title = new Dictionary<string, object>();
            Dictionary<string, object> series = new Dictionary<string, object>();
            Dictionary<string, object> dicEncode = new Dictionary<string, object>();
            Dictionary<string, object> label = new Dictionary<string, object>();
            Dictionary<string, object> legend = new Dictionary<string, object>();

            string titleText = "";
            string sql = "";
            switch (type)
            {
                case "1":
                    titleText = "Bug状态";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmmc, a.xmbm, a.status, count(a.Status)Quantity, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a
where a.nodeorder > 0
                                                                                   group by a.xmmc, a.xmbm, a.Status
) b on a.xmbm = b.xmbm and a.type = b.type and a.item = b.status
where a.type = '{1}'and a.xmbm = '{2}'", titleText, type, xmbm);
                    break;
                case "2":
                    titleText = "问题发现阶段";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmmc, a.xmbm, a.wtfxjd, count(a.wtfxjd)Quantity, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a
where a.nodeorder > 0
                                                                                   group by a.xmmc, a.xmbm, a.wtfxjd
) b on a.xmbm = b.xmbm and a.type = b.type  and a.item=b.wtfxjd
where a.type = '{1}'and a.xmbm = '{2}'", titleText, type, xmbm);
                    break;
                case "3":
                    titleText = "故障大类";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmbm,a.xmmc,count(a.sjfl)Quantity,a.parenttype, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.sjfl,a.parenttype
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.parenttype
where a.type = '{1}'and a.xmbm = '{2}'", titleText, type, xmbm);
                    break;
                case "4":
                    titleText = "故障小类";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmbm,a.xmmc,count(a.flid)Quantity,a.childtype, '{1}'Type, '{0}'TypeName from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.flid,a.childtype
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.childtype
where a.type = '{1}'and a.xmbm = '{2}'", titleText, type, xmbm);
                    break;
                case "5":
                    titleText = "严重程度";
                    sql = string.Format(@"select '{0}' Item,'数量' Quantity from dual  
                            union all select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a 
left join
(
select a.xmbm,a.xmmc,count(a.yccdmc)Quantity,a.yccdmc,'{1}'Type, '{0}'TypeName from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.yccdmc
) b on a.xmbm=b.xmbm and a.type=b.type and a.item=b.yccdmc
where a.type = '{1}'and a.xmbm = '{2}'", titleText, type, xmbm);
                    break;
                default:
                    break;
            }
            if (sfsx == "0")
            {
                sql += " sfsx=0";
            }
            legend.Add("top", "8%");
            model.legend.Add(legend);
            title.Add("text", titleText);
            series.Add("type", "pie");
            series.Add("radius", "65%");
            label.Add("formatter", "{b}:{@[1]}({d}%)");
            label.Add("fontSize", "24px");
            series.Add("label", label);
            dicEncode.Add("itemName", titleText);
            dicEncode.Add("value", "数量");
            series.Add("encode", dicEncode);
            series.Add("datasetIndex", 0);
            model.title.Add(title);
            model.series.Add(series);
            DataSet ds = db.Query(sql);
            List<List<string>> liData = new List<List<string>>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<string> temp = new List<string>();
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    temp.Add(ds.Tables[0].Rows[i][j].ToString());
                }
                liData.Add(temp);
            }
            Dictionary<string, object> dicTemp = new Dictionary<string, object>();
            dicTemp.Add("source", liData);
            model.dataset.Add(dicTemp);
            return model;
        }
        #endregion
        public EChartModel GetSeries(IDataBase db, string xmbm, string type, string sfsx)
        {
            EChartModel model = new EChartModel();
            string domid = "main" + type;
            model.DomID = domid;
            //title
            //JObject title = new JObject();
            //JObject series = new JObject();
            Dictionary<string, object> title = new Dictionary<string, object>();
            Dictionary<string, object> series = new Dictionary<string, object>();
            Dictionary<string, object> dicEncode = new Dictionary<string, object>();
            Dictionary<string, object> label = new Dictionary<string, object>();
            //model.xAxis
            //model.yAxis
            //model.grid
            string sql = "";
            switch (type)
            {
                case "1":
                    title.Add("text", "Bug状态");
                    model.title.Add(title);
                    //title.Add("text", "Bug状态");
                    series.Add("type", "pie");
                    series.Add("radius", "15%");
                    series.Add("datasetIndex", 0);
                    dicEncode.Add("itemName", "Bug状态");
                    dicEncode.Add("value", "数量");
                    series.Add("encode", dicEncode);
                    label.Add("formatter", "{b}|{@[1]}个{d}%");
                    series.Add("label", label);
                    model.series.Add(series);
                    sql = @"select 'Bug状态' Item,'数量' Quantity from dual
union all
select a.item,to_char(nvl(b.Quantity, 0))Quantity from v_bugfree_items a
left join
(
select a.xmmc, a.xmbm, a.status, count(a.Status)Quantity, '1'Type, 'Bug状态'TypeName from v_bugfreeinfo a
where a.nodeorder > 0
                                                                                   group by a.xmmc, a.xmbm, a.Status
) b on a.xmbm = b.xmbm and a.type = b.type and a.item = b.status
where a.type = '1'and a.xmbm = '" + xmbm + "'";
                    break;
                case "2":
                    title.Add("text", "问题发现阶段");
                    model.title.Add(title);
                    //title.Add("text", "Bug状态");
                    series.Add("type", "pie");
                    series.Add("radius", "15%");
                    series.Add("datasetIndex", 0);
                    dicEncode.Add("itemName", "问题发现阶段");
                    dicEncode.Add("value", "数量");
                    series.Add("encode", dicEncode);
                    label.Add("formatter", "{b}:{@[1]}({d})%");
                    series.Add("label", label);
                    model.series.Add(series);
                    sql = @"select '问题发现阶段' Item,'数量' Quantity from dual
union all
select a.item,to_char(nvl(b.Quantity,0))Quantity from v_bugfree_items a 
left join 
(
select a.xmbm,a.xmmc,count(a.wtfxjd)Quantity,a.wtfxjd,'2'Type,'问题发现阶段'TypeName 
from v_bugfreeinfo a 
where a.nodeorder>0 group by a.xmbm,a.xmmc,a.wtfxjd
) b 
on a.xmbm=b.xmbm and a.type=b.type and a.id=b.wtfxjd
where a.type='2'and a.xmbm = '" + xmbm + "'";
                    break;
                default:
                    break;
            }
            if (sfsx == "0")
            {
                sql += " sfsx=0";
            }
            DataSet ds = db.Query(sql);
            List<List<string>> liData = new List<List<string>>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                List<string> temp = new List<string>();
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    temp.Add(ds.Tables[0].Rows[i][j].ToString());
                }
                liData.Add(temp);
            }
            Dictionary<string, object> dicTemp = new Dictionary<string, object>();
            dicTemp.Add("source", liData);
            model.dataset.Add(dicTemp);

            return model;
        }
    }
}
