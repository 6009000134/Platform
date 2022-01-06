using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_Columns
	public class Sys_ColumnsModel
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
		/// CreatedBy
        /// </summary>		
		private string _createdby;
				public string CreatedBy
        {   
            get{ return _createdby; }
            set{ _createdby =value; }
        } 
		       
		/// <summary>
		/// CreatedDate
        /// </summary>		
		private DateTime _createddate;
		public DateTime CreatedDate
        {   
            get
            {
                if (_createddate == DateTime.MinValue || _createddate  == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _createddate;
                }
            }
            set{ _createddate =value; }
        } 
				       
		/// <summary>
		/// UpdatedBy
        /// </summary>		
		private string _updatedby;
				public string UpdatedBy
        {   
            get{ return _updatedby; }
            set{ _updatedby =value; }
        } 
		       
		/// <summary>
		/// UpdatedDate
        /// </summary>		
		private DateTime _updateddate;
		public DateTime UpdatedDate
        {   
            get
            {
                if (_updateddate == DateTime.MinValue || _updateddate  == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _updateddate;
                }
            }
            set{ _updateddate =value; }
        } 
				       
		/// <summary>
		/// TableID
        /// </summary>		
		private int _tableid;
				public int TableID
        {   
            get{ return _tableid; }
            set{ _tableid =value; }
        } 
		       
		/// <summary>
		/// TableName
        /// </summary>		
		private string _tablename;
				public string TableName
        {   
            get{ return _tablename; }
            set{ _tablename =value; }
        } 
		       
		/// <summary>
		/// ColumnName
        /// </summary>		
		private string _columnname;
				public string ColumnName
        {   
            get{ return _columnname; }
            set{ _columnname =value; }
        } 
		       
		/// <summary>
		/// ColumnName_EN
        /// </summary>		
		private string _columnname_en;
				public string ColumnName_EN
        {   
            get{ return _columnname_en; }
            set{ _columnname_en =value; }
        } 
		       
		/// <summary>
		/// ColumnName_CN
        /// </summary>		
		private string _columnname_cn;
				public string ColumnName_CN
        {   
            get{ return _columnname_cn; }
            set{ _columnname_cn =value; }
        } 
		       
		/// <summary>
		/// ColumnType
        /// </summary>		
		private string _columntype;
				public string ColumnType
        {   
            get{ return _columntype; }
            set{ _columntype =value; }
        } 
		       
		/// <summary>
		/// Size
        /// </summary>		
		private int _size;
				public int Size
        {   
            get{ return _size; }
            set{ _size =value; }
        } 
		       
		/// <summary>
		/// IsNullable
        /// </summary>		
		private bool _isnullable;
				public bool IsNullable
        {   
            get{ return _isnullable; }
            set{ _isnullable =value; }
        } 
		       
		/// <summary>
		/// DefaultValue
        /// </summary>		
		private string _defaultvalue;
				public string DefaultValue
        {   
            get{ return _defaultvalue; }
            set{ _defaultvalue =value; }
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

        public int OrderNo
        {
            get
            {
                return orderNo;
            }

            set
            {
                orderNo = value;
            }
        }

        private int orderNo;
		   
	}
}