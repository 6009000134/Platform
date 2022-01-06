using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.Model
{
    //Sys_Tables
    public class Sys_TablesModel
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        /// <summary>
        /// 表ID
        /// </summary>        
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// CreatedBy
        /// </summary>		
        private string _createdby;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }

        /// <summary>
        /// CreatedDate
        /// </summary>		
        private DateTime? _createddate;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreatedDate
        {
            get
            {
                return _createddate;
            }
            set
            {
                _createddate = value;

            }
        }

        /// <summary>
        /// UpdatedBy
        /// </summary>		
        private string _updatedby;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdatedBy
        {
            get { return _updatedby; }
            set { _updatedby = value; }
        }

        /// <summary>
        /// UpdatedDate
        /// </summary>		
        private DateTime? _updateddate;
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdatedDate
        {
            get
            {
                return _updateddate;
            }
            set { _updateddate = value; }
        }


        /// <summary>
        /// TableName
        /// </summary>		
        private string _tablename;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return _tablename; }
            set { _tablename = value; }
        }

        /// <summary>
        /// TableName_EN
        /// </summary>		
        private string _tablename_en;
        /// <summary>
        /// 表中文名称
        /// </summary>
        public string TableName_EN
        {
            get { return _tablename_en; }
            set { _tablename_en = value; }
        }

        /// <summary>
        /// TableName_CN
        /// </summary>		
        private string _tablename_cn;
        /// <summary>
        /// 表英文名
        /// </summary>
        public string TableName_CN
        {
            get { return _tablename_cn; }
            set { _tablename_cn = value; }
        }

        /// <summary>
        /// Remark
        /// </summary>		
        private string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private int _dbType;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public int DBType
        {
            get
            {
                return _dbType;
            }

            set
            {
                _dbType = value;
            }
        }

        private string _dbName;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName
        {
            get
            {
                return _dbName;
            }

            set
            {
                _dbName = value;
            }
        }
        private string _dbTypeCode;
        /// <summary>
        /// 数据库类型（SqlServer/MySql/Oracle）
        /// </summary>
        public string DBTypeCode
        {
            get
            {
                return _dbTypeCode;
            }

            set
            {
                _dbTypeCode = value;
            }
        }

        private string _dbCon;
        /// <summary>
        /// 数据库连接名称（SqlServer/MySql/Oracle）
        /// </summary>
        public string DBCon
        {
            get
            {
                return _dbCon;
            }

            set
            {
                _dbCon = value;
            }
        }

    }
}