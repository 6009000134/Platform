using MyPlatform.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class TodoBLL:BLLBase
    {
        IDataBase db;
        public TodoBLL()
        {
            db = DBHelperFactory.Create("DiaryCon");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public bool Add(Dictionary<string,string> dic)
        {
            string sql =(@"INSERT INTO dbo.Todos
        ( CreatedBy ,
          CreatedDate ,
          UpdatedBy ,
          UpdatedDate ,
          Content ,
          Status
        )
VALUES  ( @CreatedBy , 
          GETDATE() ,
          @UpdatedBy , 
          GETDATE() , 
          @Content , 
          0 
        )");
            SqlParameter[] pars = { new SqlParameter("@CreatedBy", dic["CreatedBy"]), new SqlParameter("@UpdatedBy", dic["UpdatedBy"]), new SqlParameter("@Content", dic["Content"])};
            return db.ExecuteNonQuery(sql, pars)>0;
        }
        public DataSet GetList()
        {
            string sql = "select * from todos";
            return db.Query(sql);
        }
    }
}
