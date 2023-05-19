using MyPlatform.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlatform.Model;
using MyPlatform.DBUtility;
using System.Data;

namespace MyPlatform.SQLServerDAL
{
    public class QueryObjectDAL : IQueryObject
    {
        /// <summary>
        /// 获取查询对象字段信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.QueryObjectModel GetQueryObjectData(IDataBase db, int id)
        {
            MyPlatform.Model.QueryObjectModel o = new Model.QueryObjectModel();
            try
            {
                DataSet ds = new DataSet();
                string sql = string.Format("select * from Sys_Query where id={0}", id);
                string sql2 = string.Format("select * from Sys_QueryDetail where tableid={0}", id);
                List<string> liSqls = new List<string>();
                liSqls.Add(sql);
                liSqls.Add(sql2);
                ds = db.Query(liSqls);
                if (ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    //表信息
                    o.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    o.ShortName = ds.Tables[0].Rows[0]["ShortName"].ToString();
                    o.DBCon = ds.Tables[0].Rows[0]["DBCon"].ToString();
                    o.DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    o.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    o.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                    //列信息
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {

                        MyPlatform.Model.QueryObjectDetail detail = new QueryObjectDetail();
                        detail.CreatedBy = ds.Tables[1].Rows[i]["QueryID"].ToString();
                        detail.CreatedDate = Convert.ToDateTime(ds.Tables[1].Rows[i]["CreatedDate"].ToString());
                        detail.DBCon = ds.Tables[1].Rows[i]["DBCon"].ToString();
                        detail.DisplayName = ds.Tables[1].Rows[i]["DisplayName"].ToString();
                        detail.ID = Convert.ToInt32(ds.Tables[1].Rows[i]["ID"]);
                        detail.Name = ds.Tables[1].Rows[i]["Name"].ToString();
                        detail.Precision = Convert.ToInt32(ds.Tables[1].Rows[i]["Precision"]);
                        detail.QueryID = ds.Tables[1].Rows[i]["QueryID"].ToString();
                        detail.Size = Convert.ToInt32(ds.Tables[1].Rows[i]["Size"]);
                        detail.Type = ds.Tables[1].Rows[i]["Type"].ToString();
                        detail.UpdatedBy = ds.Tables[1].Rows[i]["UpdatedBy"].ToString();
                        detail.UpdatedDate = Convert.ToDateTime(ds.Tables[1].Rows[i]["UpdatedDate"].ToString());
                        o.Detail.Add(detail);
                    }
                }
                else
                {
                    throw new Exception("查不到查询对象信息");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return o;
        }
        /// <summary>
        /// 获取查询列表,TODO:分页
        /// </summary>
        /// <param name="db"></param>
        /// <param name="objectInfo"></param>
        /// <returns></returns>
        public DataSet GetList(IDataBase db, MyPlatform.Model.QueryObjectModel objectInfo)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "";
                switch (db.DBType)
                {
                    case DBEnum.SqlServer:
                        sql = "select ";
                        for (int i = 0; i < objectInfo.Detail.Count; i++)
                        {
                            sql += objectInfo.Detail[i].Name + ",";
                        }
                        sql = sql.TrimEnd(',') + " from " + objectInfo.Name;
                        break;
                    case DBEnum.MySql:
                        break;
                    case DBEnum.Oracle:
                        break;
                    default:
                        break;
                }
                ds = db.Query(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
