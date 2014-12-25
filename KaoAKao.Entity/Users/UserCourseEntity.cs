/**  版本信息模板在安装目录下，可自行修改。
* UserCourse.cs
*
* 功 能： N/A
* 类 名： UserCourse
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:57   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;

namespace KaoAKao.Entity
{
	/// <summary>
	/// UserCourse:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserCourseEntity
	{
        public UserCourseEntity()
		{}
		#region Model
		private int _id;
		private string _userid;
		private string _courseid;
		private int? _type;
		private DateTime? _createdate= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.UserID = cl.Contains("UserID") && dr["UserID"] != DBNull.Value ? dr["UserID"].ToString() : "";
            this.CourseID = cl.Contains("CourseID") && dr["CourseID"] != DBNull.Value ? dr["CourseID"].ToString() : "";
            this.Type = cl.Contains("Type") && dr["Type"] != DBNull.Value ? Convert.ToInt32(dr["Type"]) : 0;
            this.CreateDate = cl.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : DateTime.MinValue;
        }
		#endregion Model

	}
}

