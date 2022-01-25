using System;
using System.Data;
using System.Collections.Generic;
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
using MyPlatform.DBUtility;

namespace MyPlatform.BLL
{
    //Sys_Columns
    public partial class Sys_ColumnsBLL
    {
        private readonly ISys_Columns dal = DataAccess.CreateInstance<ISys_Columns>("Sys_ColumnsDAL");
        public Sys_ColumnsBLL()
        { }
        public ReturnData AddColumn(Model.Sys_ColumnsModel model)
        {
            ReturnData result = new ReturnData();
            try
            {
                //根据tableid获取数据库连接名
                Sys_TablesBLL t = new Sys_TablesBLL();
                ReturnData tableInfo = t.GetDetail(model.TableID, new Pagination() { pageSize = 10, pageIndex = 1 });
                DataSet ds = (DataSet)tableInfo.D;
                string str = ds.Tables[0].Rows[0].ToJson();
                MyPlatform.Model.Sys_TablesModel table = ModelConverter<MyPlatform.Model.Sys_TablesModel>.ConvertToModelEntity(ds.Tables[0]);
                model.TableName = table.TableName;
                IDataBase db = DBHelperFactory.Create(table.DBCon);
                result=dal.Add(db,model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public DataSet GetList(int tableID)
        {
            return dal.GetList(tableID);
        }

    }
}