using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.DBUtility
{
    public interface ICommandData
    {

        string CommandText { get; set; }
        IDbConnection connection { get; set; }
        IDbCommand cmd { get; set; }
        IDataParameter[] Paras{ get; set; }
        
    }
}
