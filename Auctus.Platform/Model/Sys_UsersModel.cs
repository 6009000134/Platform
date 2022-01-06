using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.Model
{
    //Sys_Users
    public class Sys_UsersModel
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// CreatedBy
        /// </summary>		
        private string _createdby;
        public string CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }

        /// <summary>
        /// CreatedDate
        /// </summary>		
        private DateTime? _createddate;
        public DateTime? CreatedDate
        {
            get
            {
                //_createddate= System.Data.SqlTypes.SqlDateTime.MinValue.Value;
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
        public string UpdatedBy
        {
            get { return _updatedby; }
            set { _updatedby = value; }
        }

        /// <summary>
        /// UpdatedDate
        /// </summary>		
        private DateTime? _updateddate;
        public DateTime? UpdatedDate
        {
            get
            {
                return _updateddate;
            }
            set { _updateddate = value; }
        }

        /// <summary>
        /// Deleted
        /// </summary>		
        private int _deleted;
        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        /// <summary>
        /// Account
        /// </summary>		
        private string _account;
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        /// <summary>
        /// Password
        /// </summary>		
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// UserName
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }


    }
}