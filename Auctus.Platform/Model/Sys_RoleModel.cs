using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_Role
	public class Sys_RoleModel
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
		/// PID
        /// </summary>		
		private int _pid;
				public int PID
        {   
            get{ return _pid; }
            set{ _pid =value; }
        } 
		       
		/// <summary>
		/// RoleName
        /// </summary>		
		private string _rolename;
				public string RoleName
        {   
            get{ return _rolename; }
            set{ _rolename =value; }
        } 
		       
		/// <summary>
		/// Remark
        /// </summary>		
		private string _remark;
				public string Remark
        {   
            get{ return _remark; }
            set{ _remark =value; }
        } 
		       
		   
	}
}