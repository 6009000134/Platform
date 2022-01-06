using MyPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class Holiday
    {

        private void GetHolidays()
        {
            string data = HttpMethod.PostMethod("https://www.mxnzp.com/api/holiday/list/year/2020?app_id=akotsopmynpyvepi&app_secret=bUc0YThNcXZZUTVCVjN0OFp0Z1UyUT09", "");
            data1 dd = JSONUtil.ParseFromJson<data1>(data);
            List<string> sqls = new List<string>();
            List<string> dates = new List<string>();
            List<string> dates2 = new List<string>();
            dates2.Add("2020-06-13");
            dates2.Add("2020-06-14");
            if (dd.data.Count > 0)
            {
                for (int i = 0; i < dd.data.Count; i++)
                {
                    if (dd.data[i].days.Count > 0)
                    {
                        for (int j = 0; j < dd.data[i].days.Count; j++)
                        {
                            if (dd.data[i].days[j].type > 0)
                            {
                                dates.Add(dd.data[i].days[j].date);
                            }
                        }
                    }
                }
            }
            if (dates.Count > 0)
            {
                for (int i = 0; i < dates.Count; i++)
                {
                    string sql = "insert into auctus_temp values ('" + dates[i] + "');";
                    sqls.Add(sql);
                }
            }
            List<string> r = dates.Where(a => !dates2.Exists(t => t == a)).ToList();
            List<string> rs = dates.Where(a => a == "2020-06-13").ToList();

            //SqlConnection con = new SqlConnection("Data Source=192.168.1.81;Initial Catalog=AuctusERP;User ID=sa;Password=db@auctus998.;");
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //con.Open();
            //for (int i = 0; i < sqls.Count; i++)
            //{
            //    cmd.CommandText = sqls[i];
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.Clear();
            //}
            //con.Close();
            Console.ReadLine();
        }
    }

    #region 假期返回数据
    class data1
    {
        public string code { get; set; }
        public string msg { get; set; }
        public List<data2> data { get; set; }
    }
    class data2
    {
        public string month { get; set; }

        public string year { get; set; }
        public List<data3> days { get; set; }
    }
    class data3
    {
        public string date { get; set; }
        public int type { get; set; }

    }
    #endregion
}
