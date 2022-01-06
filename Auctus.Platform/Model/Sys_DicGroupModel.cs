using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_DicGroup
	public class Sys_DicGroupModel
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
		/// 键值对组编码
        /// </summary>		
		private string _code;
				public string Code
        {   
            get{ return _code; }
            set{ _code =value; }
        } 
		       
		/// <summary>
		/// 键值对组名称
        /// </summary>		
		private string _name;
				public string Name
        {   
            get{ return _name; }
            set{ _name =value; }
        } 
		       
		/// <summary>
		/// 是否有效
        /// </summary>		
		private bool _isactive;
				public bool IsActive
        {   
            get{ return _isactive; }
            set{ _isactive =value; }
        } 
		       
		/// <summary>
		/// 备注
        /// </summary>		
		private string _remark;
				public string Remark
        {   
            get{ return _remark; }
            set{ _remark =value; }
        } 
		       
		   
	}
}