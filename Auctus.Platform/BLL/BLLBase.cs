using MyPlatform.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.BLL
{
    public class BLLBase
    {
        public IDataBase currentDB;
        public string con;
        public BLLBase()
        {         
        }
        public void GetDataBase(string con)
        {
            currentDB = DBHelperFactory.Create(con);
        }
    }
}
