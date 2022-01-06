﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.Model
{
    //Sys_Menu
    public class Sys_MenuModel
    {
        public Sys_MenuModel()
        {
        }
        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        /// <summary>
        /// ID
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
        private DateTime _createddate;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate
        {
            get
            {
                if (_createddate == DateTime.MinValue || _createddate == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _createddate;
                }
            }
            set { _createddate = value; }
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
        private DateTime _updateddate;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedDate
        {
            get
            {
                if (_updateddate == DateTime.MinValue || _updateddate == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _updateddate;
                }
            }
            set { _updateddate = value; }
        }

        /// <summary>
        /// MenuName
        /// </summary>		
        private string _menuname;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get { return _menuname; }
            set { _menuname = value; }
        }

        /// <summary>
        /// Uri
        /// </summary>		
        private string _uri;
        /// <summary>
        /// Url
        /// </summary>
        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        /// <summary>
        /// ParentID
        /// </summary>		
        private int _parentid;
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// 菜单
        /// </summary>
        public Sys_VueRouterModel Router { get; set; }

    }
}