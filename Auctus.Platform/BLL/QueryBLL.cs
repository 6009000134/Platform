using MyPlatform.DBUtility;
using MyPlatform.IDAL;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class QueryBLL
    {
        IQueryObject dal = DALFactory.DataAccess.CreateInstance<IQueryObject>("QueryObjectDAL");
        /// <summary>
        /// 查询“查询视图”数据集
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public DataSet GetList(QueryObjectModel o)
        {
            DataSet ds = new DataSet();
            try
            {
                //获取查询对象信息
                IDataBase db = DBUtility.DBHelperFactory.Create("Default");
                //获取查询结果
                ds = GetDataList(o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        /// <summary>
        /// 获取查询视图对象信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QueryObjectModel GetQueryObjectData(int id)
        {
            QueryObjectModel o;
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create("Default");
                o = dal.GetQueryObjectData(db, id);
                if (o == null)
                {
                    throw new Exception("查询视图不存在");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return o;
        }
        /// <summary>
        /// 查询查询视图数据集
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private DataSet GetDataList(QueryObjectModel o)
        {
            //查询数据
            DataSet ds = new DataSet();
            try
            {
                IDataBase db = DBUtility.DBHelperFactory.Create(o.DBCon);
                ds = dal.GetList(db, o);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
