using MyPlatform.DBUtility;
using MyPlatform.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.IDAL
{
    public interface IQueryObject
    {
        QueryObjectModel GetQueryObjectData(IDataBase db,int id);
        DataSet GetList(IDataBase db, MyPlatform.Model.QueryObjectModel objectInfo);
    }
}
