using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class MOSchedule
    {
        public void GetData()
        {
            try
            {
                //SqlConnection con = new SqlConnection("server = 192.168.20.46; database = TT4; uid = sa; pwd = Oa@u9");
                SqlConnection con = new SqlConnection("server = 192.168.1.81; database = AuctusERP; uid = sa; pwd = db@auctus998.");
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from vw_tempDocInfo";
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dtDocInfo = new DataTable();
                sd.Fill(dtDocInfo);
                dtDocInfo.PrimaryKey = new DataColumn[] { dtDocInfo.Columns["DocNo"] };
                dtDocInfo.Columns.Add(new DataColumn("Remark"));

                cmd.CommandText = "select * from vw_tempWh";
                SqlDataAdapter sd2 = new SqlDataAdapter(cmd);
                DataTable dtWh = new DataTable();
                sd.Fill(dtWh);
                dtWh.PrimaryKey = new DataColumn[] { dtWh.Columns["Itemmaster"] };

                cmd.CommandText = "select * from vw_tempSO";
                SqlDataAdapter sd3 = new SqlDataAdapter(cmd);
                DataTable dtSO = new DataTable();
                sd.Fill(dtSO);
                dtSO.PrimaryKey = new DataColumn[] { dtSO.Columns["DocNo"], dtSO.Columns["DocLineNo"], dtSO.Columns["DocSubLineNo"] };

                cmd.CommandText = "select * from vw_PickInfo";
                SqlDataAdapter sd4 = new SqlDataAdapter(cmd);
                DataTable dtPickInfo = new DataTable();
                sd.Fill(dtPickInfo);
                dtPickInfo.PrimaryKey = new DataColumn[] { dtPickInfo.Columns["DocNo"], dtPickInfo.Columns["DocLineNo"] };
                con.Close();

                DataTable dtResult = new DataTable("result");
                DataColumn[] dc = new DataColumn[] {
                    new DataColumn("ID"),new DataColumn("RN"),new DataColumn("lv"),new DataColumn("SO_DocNo"),new DataColumn("DocLineNo"),new DataColumn("DocSubLineNo"),new DataColumn("Code"),new DataColumn("Name")
                    ,new DataColumn("DemandType"),new DataColumn("SO_ReqDate"),new DataColumn("SO_ReqQtyTU")
                    ,new DataColumn("DocNo1"),new DataColumn("Itemmaster1"),new DataColumn("Code1"),new DataColumn("Name1"),new DataColumn("ProductQty1")
                    ,new DataColumn("UnCompleteQty1"),new DataColumn("UseQty1"),new DataColumn("StartDate1"),new DataColumn("CompleteDate1")
                    ,new DataColumn("AdviseStartDate1"),new DataColumn("AdviseCompleteDate1"),new DataColumn("Remark1"),new DataColumn("PickLineNo1")
                    ,new DataColumn("DocNo2"),new DataColumn("Itemmaster2"),new DataColumn("Code2"),new DataColumn("Name2"),new DataColumn("ProductQty2")
                    ,new DataColumn("UnCompleteQty2"),new DataColumn("UseQty2"),new DataColumn("StartDate2"),new DataColumn("CompleteDate2")
                    ,new DataColumn("AdviseStartDate2"),new DataColumn("AdviseCompleteDate2"),new DataColumn("Remark2"),new DataColumn("PickLineNo2")
                    ,new DataColumn("DocNo3"),new DataColumn("Itemmaster3"),new DataColumn("Code3"),new DataColumn("Name3"),new DataColumn("ProductQty3")
                    ,new DataColumn("UnCompleteQty3"),new DataColumn("UseQty3"),new DataColumn("StartDate3"),new DataColumn("CompleteDate3")
                    ,new DataColumn("AdviseStartDate3"),new DataColumn("AdviseCompleteDate3"),new DataColumn("Remark3"),new DataColumn("PickLineNo3")
                    ,new DataColumn("DocNo4"),new DataColumn("Itemmaster4"),new DataColumn("Code4"),new DataColumn("Name4"),new DataColumn("ProductQty4")
                    ,new DataColumn("UnCompleteQty4"),new DataColumn("UseQty4"),new DataColumn("StartDate4"),new DataColumn("CompleteDate4")
                    ,new DataColumn("AdviseStartDate4"),new DataColumn("AdviseCompleteDate4"),new DataColumn("Remark4"),new DataColumn("PickLineNo4")
                    ,new DataColumn("DocNo5"),new DataColumn("Itemmaster5"),new DataColumn("Code5"),new DataColumn("Name5"),new DataColumn("ProductQty5")
                    ,new DataColumn("UnCompleteQty5"),new DataColumn("UseQty5"),new DataColumn("StartDate5"),new DataColumn("CompleteDate5")
                    ,new DataColumn("AdviseStartDate5"),new DataColumn("AdviseCompleteDate5"),new DataColumn("Remark5"),new DataColumn("PickLineNo5")
                    ,new DataColumn("DocNo6"),new DataColumn("Itemmaster6"),new DataColumn("Code6"),new DataColumn("Name6"),new DataColumn("ProductQty6")
                    ,new DataColumn("UnCompleteQty6"),new DataColumn("UseQty6"),new DataColumn("StartDate6"),new DataColumn("CompleteDate6")
                    ,new DataColumn("AdviseStartDate6"),new DataColumn("AdviseCompleteDate6"),new DataColumn("Remark6"),new DataColumn("PickLineNo6")
                    ,new DataColumn("DocNo7"),new DataColumn("Itemmaster7"),new DataColumn("Code7"),new DataColumn("Name7"),new DataColumn("ProductQty7")
                    ,new DataColumn("UnCompleteQty7"),new DataColumn("UseQty7"),new DataColumn("StartDate7"),new DataColumn("CompleteDate7")
                    ,new DataColumn("AdviseStartDate7"),new DataColumn("AdviseCompleteDate7"),new DataColumn("Remark7")
            };
                dtResult.Columns.AddRange(dc);

                //当前订单
                DataTable dtCurrent = new DataTable("current");
                DataColumn[] dc2 = new DataColumn[] {
                new DataColumn("CurrentDocNo1"),new DataColumn("CurrentItemmaster1"),new DataColumn("CurrentCode1"),new DataColumn("CurrentName1"),new DataColumn("CurrentProductQty1")
                    ,new DataColumn("CurrentUnCompleteQty1"),new DataColumn("CurrentUseQty1"),new DataColumn("CurrentStartDate1"),new DataColumn("CurrentCompleteDate1")
                    ,new DataColumn("CurrentAdviseStartDate1"),new DataColumn("CurrentAdviseCompleteDate1"),new DataColumn("CurrentRemark1")
            };
                dtCurrent.Columns.AddRange(dc2);

                int id = 0;
                for (int i = 0; i < dtSO.Rows.Count; i++)//SO、FO排程
                {
                    //DataRow drSO = dtResult.NewRow();
                    id += 1;
                    DataRow dtResultRow = dtResult.NewRow();
                    dtResultRow["ID"] = id;
                    dtResultRow["lv"] = 0;
                    dtResultRow["RN"] = dtSO.Rows[i]["RN"];
                    dtResultRow["SO_DocNo"] = dtSO.Rows[i]["DocNo"];
                    dtResultRow["DocLineNo"] = dtSO.Rows[i]["DocLineNo"];
                    dtResultRow["DocSubLineNo"] = dtSO.Rows[i]["DocSubLineNo"];
                    dtResultRow["Code"] = dtSO.Rows[i]["Code"];
                    dtResultRow["Name"] = dtSO.Rows[i]["Name"];
                    dtResultRow["SO_ReqDate"] = dtSO.Rows[i]["ReqDate"];
                    dtResultRow["SO_ReqQtyTU"] = dtSO.Rows[i]["ReqQtyTU"];
                    dtResultRow["DemandType"] = dtSO.Rows[i]["DemandType"];

                    dtResult.Rows.Add(dtResultRow);
                    int needQty = Convert.ToInt32(dtSO.Rows[i]["ReqQtyTU"]);
                    //查库存数据
                    DataRow[] drWh = dtWh.Select("Itemmaster='" + dtSO.Rows[i]["Itemmaster"] + "' and StoreQty>0");
                    bool IsWhEnough = false;//库存是否能满足需求
                    if (drWh.Count() > 0)
                    {
                        int whQty = Convert.ToInt32(drWh[0]["StoreQty"]);
                        if (whQty > 0)//有库存
                        {
                            id += 1;
                            dtResultRow = dtResult.NewRow();
                            dtResultRow["ID"] = id;
                            dtResultRow["lv"] = 1;
                            dtResultRow["RN"] = dtSO.Rows[i]["RN"];
                            dtResultRow["Code1"] = dtSO.Rows[i]["Code"];
                            dtResultRow["Name1"] = dtSO.Rows[i]["Name"];
                            dtResultRow["ProductQty1"] = whQty;
                            if (whQty >= needQty)//库存可以满足需求数量
                            {
                                IsWhEnough = true;
                                dtResultRow["DocNo1"] = "库存";
                                dtResultRow["UseQty1"] = needQty;
                                dtResult.Rows.Add(dtResultRow);
                                //更新SO需求数量和库存数量
                                needQty = 0;
                                drWh[0]["StoreQty"] = whQty - needQty;
                                IsWhEnough = true;
                            }
                            else//库存无法满足需求数量
                            {
                                dtResultRow["DocNo1"] = "库存不足";
                                dtResultRow["UseQty1"] = whQty;
                                dtResult.Rows.Add(dtResultRow);
                                //更新SO需求数量和库存数量
                                needQty = needQty - whQty;
                                drWh[0]["StoreQty"] = 0;
                                IsWhEnough = false;
                            }
                        }
                    }
                    if (!IsWhEnough)//库存无法满足需求数量时，计算工单提供数量
                    {
                        bool IsMOEnough = false;
                        //查需求分类号一致的工单
                        if (dtSO.Rows[i]["DemandType"].ToString() != "-1")
                        {
                            DataRow[] drs2 = dtDocInfo.Select(" Itemmaster='" + dtSO.Rows[i]["Itemmaster"] + "' and DemandCode='" + dtSO.Rows[i]["DemandType"] + "' and CanAffordQty>0", "OrderNo");
                            if (drs2.Count() > 0)
                            {
                                foreach (DataRow drItem in drs2)
                                {
                                    int canAffordQty = Convert.ToInt32(drItem["CanAffordQty"]);//工单可提供数量
                                    int affordQty = 0;//提供数量
                                    id += 1;
                                    dtResultRow = dtResult.NewRow();
                                    dtResultRow["ID"] = id;
                                    dtResultRow["RN"] = dtSO.Rows[i]["RN"];
                                    dtResultRow["DocNo1"] = drItem["DocNo"];
                                    dtResultRow["Code1"] = drItem["Code"];
                                    dtResultRow["Name1"] = drItem["Name"];
                                    dtResultRow["StartDate1"] = drItem["StartDate"];
                                    dtResultRow["CompleteDate1"] = drItem["CompleteDate"];
                                    dtResultRow["AdviseStartDate1"] = Convert.ToDateTime(dtSO.Rows[i]["ReqDate"]).AddDays(-3 - Convert.ToInt32(drItem["FixedLT"]));//计划开工日期=完工日期-固定提前期
                                    dtResultRow["AdviseCompleteDate1"] = Convert.ToDateTime(dtSO.Rows[i]["ReqDate"]).AddDays(-3);//完工日期=销售交期-3
                                    dtResultRow["ProductQty1"] = drItem["Code"];
                                    dtResultRow["UnCompleteQty1"] = drItem["Code"];
                                    if (canAffordQty >= needQty && needQty > 0)//工单数量可满足需求数量
                                    {
                                        //更新SO需求数量，MO可提供数量
                                        needQty = 0;
                                        canAffordQty = canAffordQty - needQty;
                                        affordQty = needQty;
                                    }
                                    else//工单数量无法满足需求数量
                                    {
                                        //更新SO需求数量，MO可提供数量
                                        needQty = needQty - canAffordQty;
                                        canAffordQty = 0;
                                        affordQty = canAffordQty;
                                    }
                                    dtResultRow["UseQty1"] = affordQty;//工单为当前SO提供数量
                                    dtResult.Rows.Add(dtResultRow);
                                    //更新MO数据
                                    //drItem["Remark"] = drItem["Remark"].ToString() + "," + dtSO.Rows[i]["DocNo"].ToString() + dtSO.Rows[i]["DocLineNo"].ToString() + dtSO.Rows[i]["DocSubLineNo"].ToString() + "使用了" + affordQty.ToString();
                                    //drItem["CanaffordQty"] = canAffordQty;
                                    id += 1;
                                    ExpandMo(drItem, dtResultRow["AdviseStartDate1"].ToString(), 2, id, affordQty, dtDocInfo, dtWh, dtPickInfo, dtResult);
                                    if (needQty == 0)
                                    {
                                        IsMOEnough = true;
                                        break;
                                    }
                                    else
                                    {
                                        IsMOEnough = false;
                                    }
                                }
                            }
                        }
                        if (!IsMOEnough)//需求分类号工单不够用或者SO\FO无需求分类号
                        {
                            DataRow[] drs2 = dtDocInfo.Select(" Itemmaster='" + dtSO.Rows[i]["Itemmaster"] + "' and DemandCode='-1'", "OrderNo");
                            if (drs2.Count() > 0)
                            {
                                foreach (DataRow drItem in drs2)
                                {
                                    int canAffordQty = Convert.ToInt32(drItem["CanAffordQty"]);//工单可提供数量
                                    int affordQty = 0;//提供数量
                                    id += 1;
                                    dtResultRow = dtResult.NewRow();
                                    dtResultRow["ID"] = id;
                                    dtResultRow["RN"] = dtSO.Rows[i]["RN"];
                                    dtResultRow["DocNo1"] = drItem["DocNo"];
                                    dtResultRow["Code1"] = drItem["Code"];
                                    dtResultRow["Name1"] = drItem["Name"];
                                    dtResultRow["StartDate1"] = drItem["StartDate"];
                                    dtResultRow["CompleteDate1"] = drItem["CompleteDate"];
                                    dtResultRow["AdviseStartDate1"] = Convert.ToDateTime(dtSO.Rows[i]["ReqDate"]).AddDays(-3 - Convert.ToInt32(drItem["FixedLT"]));//计划开工日期=完工日期-固定提前期
                                    dtResultRow["AdviseCompleteDate1"] = Convert.ToDateTime(dtSO.Rows[i]["ReqDate"]).AddDays(-3);//完工日期=销售交期-3
                                    dtResultRow["ProductQty1"] = drItem["Code"];
                                    dtResultRow["UnCompleteQty1"] = drItem["Code"];
                                    //工单提供数量赋值
                                    if (canAffordQty >= needQty && needQty > 0)
                                    {
                                        affordQty = needQty;
                                    }
                                    else
                                    {

                                        affordQty = canAffordQty;
                                    }
                                    //更新SO需求数量，MO可提供数量
                                    needQty = needQty - affordQty;
                                    canAffordQty = canAffordQty - affordQty;
                                    //更新工单可提供数量
                                    drItem["CanAffordQty"] = canAffordQty;
                                    dtResultRow["UseQty1"] = affordQty;//工单为当前SO提供数量
                                    dtResult.Rows.Add(dtResultRow);
                                    //更新MO数据
                                    //备注在何处修改待商榷
                                    //drItem["Remark"] = drItem["Remark"].ToString() + "," + dtSO.Rows[i]["DocNo"].ToString() + dtSO.Rows[i]["DocLineNo"].ToString() + dtSO.Rows[i]["DocSubLineNo"].ToString() + "使用了" + affordQty.ToString();
                                    //drItem["CanaffordQty"] = canAffordQty;     
                                    id += 1;
                                    ExpandMo(drItem, dtResultRow["AdviseStartDate1"].ToString(), 2, id, affordQty, dtDocInfo, dtWh, dtPickInfo, dtResult);
                                    if (needQty == 0)
                                    {
                                        IsMOEnough = true;
                                        break;
                                    }
                                    else
                                    {
                                        IsMOEnough = false;
                                    }
                                }
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
                Console.WriteLine(strEx);
            }
        }

        //展BOM        
        public static void ExpandMo(DataRow drMO, string AdviseStartDate1, int lv, int id, int needQty, DataTable dtDocInfo, DataTable dtWh, DataTable dtPickInfo, DataTable dtResult)
        {
            //工单信息：工单号、工单需求数量
            //1、判断库存是否满足
            //2、库存不满足，抓出所有备料单
            //3、计算备料单数量
            //4、备料单抓取工单号号、工单需求数量，循环(1-4)
            DataRow dtResultRow = dtResult.NewRow();
            //DataRow[] drWh = dtWh.Select("Itemmaster='" + drMO["Itemmaster"] + "' and StoreQty>0");
            DataRow[] drPickInfo = dtPickInfo.Select("DocNo='" + drMO["DocNo"] + "'", "DocLineNo");//工单备料单信息
            string pickLineSeg = "PickLineNo" + (lv - 1).ToString();
            string docnoSeg = "DocNo" + lv.ToString();
            string codeSeg = "Code" + lv.ToString();
            string nameSeg = "Name" + lv.ToString();
            string productQtySeg = "ProductQty" + lv.ToString();
            string unCompleteQtySeg = "UnCompleteQty" + lv.ToString();
            string useQtySeg = "UseQty" + lv.ToString();
            string startDateSeg = "StartDate" + lv.ToString();
            string completeDateSeg = "CompleteDate" + lv.ToString();
            string adviseSD = "AdviseStartDate" + lv.ToString();
            string adviseED = "AdviseCompleteDate" + lv.ToString();
            //计算备料单剩余已发料数量是否够，不够看是否有库存，无库存再根据备料单料号找到下阶工单
            if (drPickInfo.Count() > 0)
            {
                foreach (DataRow drItem in drPickInfo)
                {
                    //计算备料需要数量
                    int needPickQty = Convert.ToInt32(Math.Ceiling(needQty * Convert.ToDouble(drItem["CQty"]) / Convert.ToDouble(drItem["PQty"])));//需求数量*用量（备料单实际需求数量/工单生产数量）
                    string pickLineNo = drItem["DocLineNo"].ToString();
                    int remainIssuedQty = Convert.ToInt32(drItem["RemainIssuedQty"]);
                    int remainReqQty = Convert.ToInt32(drItem["remainReqQty"]);
                    if (remainIssuedQty > 0)//工单已发料数量并未用完
                    {
                        dtResultRow = dtResult.NewRow();
                        id += 1;
                        if (needPickQty >= remainIssuedQty)
                        {
                            dtResultRow["ID"] = id;
                            dtResultRow[pickLineSeg] = drItem["DocLineNo"];
                            dtResultRow[docnoSeg] = "已发料数量";
                            dtResultRow[codeSeg] = drItem["Code"];
                            dtResultRow[nameSeg] = drItem["Name"];
                            dtResultRow[useQtySeg] = drItem["RemainIssuedQty"];
                            drItem["RemainIssuedQty"] = 0;
                            needPickQty = needPickQty - remainIssuedQty;
                        }
                        else
                        {
                            dtResultRow["ID"] = id;
                            dtResultRow[pickLineSeg] = drItem["DocLineNo"];
                            dtResultRow[docnoSeg] = "已发料数量";
                            dtResultRow[codeSeg] = drItem["Code"];
                            dtResultRow[nameSeg] = drItem["Name"];
                            dtResultRow[useQtySeg] = needPickQty;
                            drItem["RemainIssuedQty"] = remainIssuedQty - needPickQty;
                            needPickQty = 0;
                        }
                        dtResult.Rows.Add(dtResultRow);
                    }
                    if (needPickQty > 0)//备料单已发的多余部分无法满足需求，仍然需要备料
                    {
                        DataRow[] drWh = dtWh.Select("Itemmaster='" + drItem["CID"] + "' and StoreQty>0");
                        if (drWh.Count() > 0)
                        {
                            int whQty = Convert.ToInt32(drWh[0]["StoreQty"]);
                            dtResultRow = dtResult.NewRow();
                            id += 1;
                            if (whQty >= needPickQty)//库存够备料单
                            {
                                dtResultRow["ID"] = id;
                                dtResultRow[pickLineSeg] = drItem["DocLineNo"];
                                dtResultRow[docnoSeg] = "库存";
                                dtResultRow[codeSeg] = drItem["Code"];
                                dtResultRow[nameSeg] = drItem["Name"];
                                dtResultRow[useQtySeg] = needPickQty;
                                whQty = whQty - needPickQty;
                                needPickQty = 0;
                            }
                            else//库存不够备料单
                            {
                                dtResultRow["ID"] = id;
                                dtResultRow[pickLineSeg] = drItem["DocLineNo"];
                                dtResultRow[docnoSeg] = "库存";
                                dtResultRow[codeSeg] = drItem["Code"];
                                dtResultRow[nameSeg] = drItem["Name"];
                                dtResultRow[useQtySeg] = whQty;
                                whQty = 0;
                                needPickQty = needPickQty - whQty;
                            }
                            dtResult.Rows.Add(dtResultRow);
                            //更新库存数量
                            drWh[0]["StoreQty"] = whQty;
                        }
                        if (needPickQty > 0)//库存无法满足需求，仍需要备料
                        {
                            if (remainReqQty > 0)
                            {
                                DataRow[] drMO2 = dtDocInfo.Select("Itemmaster='" + drItem["CID"].ToString() + "' and CanAffordQty>0", "OrderNo");
                                if (drMO2.Count() > 0)
                                {
                                    foreach (DataRow drItem2 in drMO2)
                                    {
                                        int useQty = 0;
                                        if (remainReqQty > needPickQty)
                                        {
                                            useQty = needPickQty;
                                            remainReqQty = remainReqQty - needPickQty;
                                            // drItem2["RemainReqQty"] = remainReqQty;
                                            needPickQty = 0;
                                        }
                                        else
                                        {
                                            useQty = remainReqQty;
                                            needPickQty = needPickQty - remainReqQty;
                                            remainReqQty = 0;
                                            //drItem2["RemainReqQty"] = remainReqQty;
                                        }
                                        dtResultRow = dtResult.NewRow();
                                        id += 1;
                                        dtResultRow["ID"] = id;
                                        dtResultRow[pickLineSeg] = drItem["DocLineNo"];
                                        dtResultRow[docnoSeg] = drItem2["DocNo"];
                                        dtResultRow[codeSeg] = drItem2["Code"];
                                        dtResultRow[nameSeg] = drItem2["Name"];
                                        dtResultRow[productQtySeg] = drItem2["ProductQty"];
                                        dtResultRow[unCompleteQtySeg] = drItem2["UnCompleteQty"];
                                        dtResultRow[useQtySeg] = useQty;
                                        dtResultRow[startDateSeg] = drItem2["StartDate"];
                                        dtResultRow[completeDateSeg] = drItem2["CompleteDate"];
                                        dtResultRow[adviseSD] = Convert.ToDateTime(AdviseStartDate1).AddDays(-1 * Convert.ToInt32(drItem2["FixedLT"]));
                                        dtResultRow[adviseED] = AdviseStartDate1;
                                        dtResult.Rows.Add(dtResultRow);
                                        //ExpandMo(drItem, dtResultRow[adviseSD].ToString(), lv + 1, id + 1, needPickQty, dtDocInfo, dtWh, dtPickInfo, dtResult);
                                        //remainReqQty = remainReqQty - needPickQty;
                                        drItem["RemainReqQty"] = remainReqQty;
                                        //needPickQty = 0;
                                        ExpandMo(drItem2, dtResultRow[adviseSD].ToString(), lv + 1, id + 1, useQty, dtDocInfo, dtWh, dtPickInfo, dtResult);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
