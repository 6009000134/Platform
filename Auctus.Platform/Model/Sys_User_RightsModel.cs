using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_User_Rights
	public class Sys_User_RightsModel
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
				public int ID
        {   
            get{ return _id; }
            set{ _id =value; }
        } 
		       
		/// <summary>
		/// Account
        /// </summary>		
		private string _account;
				public string Account
        {   
            get{ return _account; }
            set{ _account =value; }
        } 
		       
		/// <summary>
		/// MenuID
        /// </summary>		
		private int _menuid;
				public int MenuID
        {   
            get{ return _menuid; }
            set{ _menuid =value; }
        } 
		       
		   
	}
}