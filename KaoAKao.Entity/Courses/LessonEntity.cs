/**  版本信息模板在安装目录下，可自行修改。
* Lessons.cs
*
* 功 能： N/A
* 类 名： Lessons
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:53   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;

namespace KaoAKao.Entity
{
	/// <summary>
	/// Lessons:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LessonEntity
	{
        public LessonEntity()
		{}
		#region Model
		private int _id;
		private string _lessonid;
		private string _lessonname;
		private string _courseid;
		private string _pid;
		private int? _status=1;
		private string _radiourl;
		private string _radiosize;
		private int? _viewcount=0;
		private int? _praisecount=0;
		private int? _sharecount=0;
		private int? _collectcount=0;
		private int? _sort=0;
		private int? _ishot=0;
		private DateTime? _createdate= DateTime.Now;
		private DateTime? _lastoperatedate= DateTime.Now;
		private string _keywords="";
		private string _description="";
		private string _operateip="";
		private string _operateid="";
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
		public string LessonID
		{
			set{ _lessonid=value;}
			get{return _lessonid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LessonName
		{
			set{ _lessonname=value;}
			get{return _lessonname;}
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
		public string PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RadioURL
		{
			set{ _radiourl=value;}
			get{return _radiourl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RadioSize
		{
			set{ _radiosize=value;}
			get{return _radiosize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ViewCount
		{
			set{ _viewcount=value;}
			get{return _viewcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PraiseCount
		{
			set{ _praisecount=value;}
			get{return _praisecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ShareCount
		{
			set{ _sharecount=value;}
			get{return _sharecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CollectCount
		{
			set{ _collectcount=value;}
			get{return _collectcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsHot
		{
			set{ _ishot=value;}
			get{return _ishot;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastOperateDate
		{
			set{ _lastoperatedate=value;}
			get{return _lastoperatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Keywords
		{
			set{ _keywords=value;}
			get{return _keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateIP
		{
			set{ _operateip=value;}
			get{return _operateip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateID
		{
			set{ _operateid=value;}
			get{return _operateid;}
		}

        public List<LessonEntity> ChildLessons { get; set; }

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.LessonID = cl.Contains("LessonID") && dr["LessonID"] != DBNull.Value ? dr["LessonID"].ToString() : "";
            this.LessonName = cl.Contains("LessonName") && dr["LessonName"] != DBNull.Value ? dr["LessonName"].ToString() : "";
            this.CourseID = cl.Contains("CourseID") && dr["CourseID"] != DBNull.Value ? dr["CourseID"].ToString() : "";
            this.PID = cl.Contains("PID") && dr["PID"] != DBNull.Value ? dr["PID"].ToString() : "";
            this.Status = cl.Contains("Status") && dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
            this.RadioURL = cl.Contains("RadioURL") && dr["RadioURL"] != DBNull.Value ? dr["RadioURL"].ToString() : "";
            this.RadioSize = cl.Contains("RadioSize") && dr["RadioSize"] != DBNull.Value ? dr["RadioSize"].ToString() : "";
            this.ViewCount = cl.Contains("ViewCount") && dr["ViewCount"] != DBNull.Value ? Convert.ToInt32(dr["ViewCount"]) : 0;
            this.PraiseCount = cl.Contains("PraiseCount") && dr["PraiseCount"] != DBNull.Value ? Convert.ToInt32(dr["PraiseCount"]) : 0;
            this.ShareCount = cl.Contains("ShareCount") && dr["ShareCount"] != DBNull.Value ? Convert.ToInt32(dr["ShareCount"]) : 0;
            this.CollectCount = cl.Contains("CollectCount") && dr["CollectCount"] != DBNull.Value ? Convert.ToInt32(dr["CollectCount"]) : 0;
            this.Sort = cl.Contains("Sort") && dr["Sort"] != DBNull.Value ? Convert.ToInt32(dr["Sort"]) : 0;
            this.IsHot = cl.Contains("IsHot") && dr["IsHot"] != DBNull.Value ? Convert.ToInt32(dr["IsHot"]) : 0;
            this.CreateDate = cl.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : DateTime.MinValue;
            this.LastOperateDate = cl.Contains("LastOperateDate") && dr["LastOperateDate"] != DBNull.Value ? Convert.ToDateTime(dr["LastOperateDate"]) : DateTime.MinValue;
            this.Keywords = cl.Contains("Keywords") && dr["Keywords"] != DBNull.Value ? dr["Keywords"].ToString() : "";
            this.Description = cl.Contains("Description") && dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "";
            this.OperateIP = cl.Contains("OperateIP") && dr["OperateIP"] != DBNull.Value ? dr["OperateIP"].ToString() : "";
            this.OperateID = cl.Contains("OperateID") && dr["OperateID"] != DBNull.Value ? dr["OperateID"].ToString() : "";
        }
		#endregion Model

	}
}

