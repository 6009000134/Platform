using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_Dictionary
	public class Sys_DictionaryModel
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
		/// 键值对组ID
        /// </summary>		
		private int _groupid;
				public int GroupID
        {   
            get{ return _groupid; }
            set{ _groupid =value; }
        } 
		       
		/// <summary>
		/// 编码
        /// </summary>		
		private string _code;
				public string Code
        {   
            get{ return _code; }
            set{ _code =value; }
        } 
		       
		/// <summary>
		/// 名称
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
		/// 父节点
        /// </summary>		
		private int _pid;
				public int PID
        {   
            get{ return _pid; }
            set{ _pid =value; }
        } 
		       
		/// <summary>
		/// 排序
        /// </summary>		
		private int _orderno;
				public int OrderNo
        {   
            get{ return _orderno; }
            set{ _orderno =value; }
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