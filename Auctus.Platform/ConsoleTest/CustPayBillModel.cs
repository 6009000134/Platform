using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    /// <summary>
    /// 付款单
    /// </summary>
    public class CustPayBillModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string DocNo { get; set; }
        /// <summary>
        /// OA流程ID
        /// </summary>
        public string OAFlowID { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public long OrgID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BusinessDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long Supp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayObjType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CustPayBillLine> PayBillLine { get; set; }
        /// <summary>
        /// 核币
        /// </summary>
        public int AC { get; set; }
        /// <summary>
        /// 付款币种
        /// </summary>
        public int PC { get; set; }
        /// <summary>
        /// 本币
        /// </summary>
        public string FC { get; set; }
        /// <summary>
        /// 汇率类型
        /// </summary>
        public int ERType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long BizOrg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SettleOrg { get; set; }
        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime PayDate { get; set; }
        /// <summary>
        /// 记账期间
        /// </summary>
        public long PostPeriod { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public long Transactor { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public long Dept { get; set; }
        /// <summary>
        ///  来源组织
        /// </summary>
        public long SrcBillOrg { get; set; }
        /// <summary>
        ///来源类型，默认：0-手工录入
        /// </summary>
        public string SrcType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

    }
    /// <summary>
    /// 付款单行
    /// </summary>
    public class CustPayBillLine
    {
        /// <summary>
        /// 结算方式
        /// </summary>
        public long SettlementMethod { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal SettlementFee { get; set; }
        /// <summary>
        /// 总金额=金额+手续费
        /// </summary>
        public decimal TotalMoney { get; set; }
        /// <summary>
        /// 银行账号ID
        /// </summary>
        public long PayBkAcc { get; set; }

        /// <summary>
        /// 用途：默认3（预付款）
        /// </summary>
        public string PayProperty { get; set; }
    }
}

