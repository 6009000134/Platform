using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_Role_Right
	public class Sys_Role_RightModel
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
		/// ROleID
        /// </summary>		
		private int _roleid;
				public int ROleID
        {   
            get{ return _roleid; }
            set{ _roleid =value; }
        } 
		       
		/// <summary>
		/// RightID
        /// </summary>		
		private int _rightid;
				public int RightID
        {   
            get{ return _rightid; }
            set{ _rightid =value; }
        } 
		       
		   
	}
}