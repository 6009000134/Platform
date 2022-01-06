using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_DB
		public partial class Sys_DBBLL
	{
		private readonly ISys_DB dal=DataAccess.CreateInstance<ISys_DB>("Sys_DBDAL");
		public Sys_DBBLL()
		{}
		
   
	}
}