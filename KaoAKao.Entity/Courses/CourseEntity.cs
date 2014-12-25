/**  版本信息模板在安装目录下，可自行修改。
* Courses.cs
*
* 功 能： N/A
* 类 名： Courses
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:52   N/A    初版
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
	/// Courses:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CourseEntity
	{
        public CourseEntity()
		{}
		#region Model
		private int _id;
		private Guid _courseid;
		private string _coursename="";
		private string _categoryid;
		private string _imgurl;
		private int? _status;
		private Guid _teacherid;
		private int? _limitlevel=0;
		private decimal? _price=0M;
		private decimal? _discount=1M;
		private int? _viewcount=0;
		private int? _praisecount=0;
		private int? _sharecoun=0;
		private int? _collectcount=0;
		private int? _sort;
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
		public Guid CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseName
		{
			set{ _coursename=value;}
			get{return _coursename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgURL
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
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
		public Guid TeacherID
		{
			set{ _teacherid=value;}
			get{return _teacherid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LimitLevel
		{
			set{ _limitlevel=value;}
			get{return _limitlevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Discount
		{
			set{ _discount=value;}
			get{return _discount;}
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
		public int? ShareCoun
		{
			set{ _sharecoun=value;}
			get{return _sharecoun;}
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
		#endregion Model

        public void FillData(DataRow dr)
        {
 
        }
	}
}

