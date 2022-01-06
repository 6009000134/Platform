using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_VueRouter
	public class Sys_VueRouterModel
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
		/// Path
        /// </summary>		
		private string _path;
				public string Path
        {   
            get{ return _path; }
            set{ _path =value; }
        } 
		       
		/// <summary>
		/// Name
        /// </summary>		
		private string _name;
				public string Name
        {   
            get{ return _name; }
            set{ _name =value; }
        } 
		       
		/// <summary>
		/// Meta
        /// </summary>		
		private string _meta;
				public string Meta
        {   
            get{ return _meta; }
            set{ _meta =value; }
        } 
		       
		/// <summary>
		/// Component
        /// </summary>		
		private string _component;
				public string Component
        {   
            get{ return _component; }
            set{ _component =value; }
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