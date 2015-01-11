using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace KaoAKao.Entity
{
	/// <summary>
	/// UserInteraction:用户互动
	/// </summary>
	[Serializable]
	public partial class UserInteraction
	{
		public UserInteraction()
		{}
		#region Model
		private int _id;
		private string _userid;
        private string _courseid;
		private int? _typeid;
		private string _content="";
		private string _isreply="0";
		private string _isbest="0";
		private decimal? _integral=0M;
		private int? _praisecount=0;
		private int? _replycount=0;
		private string _name="";
		private string _mobiletele="";
		private int? _replyid=0;
		private int? _originalid;
        private string _replyuserid;
		private DateTime? _createdate= DateTime.Now;
		private string _operateip="";
		private string _operateid="";
		private int? _status=1;
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
		public int? TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsReply
		{
			set{ _isreply=value;}
			get{return _isreply;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsBest
		{
			set{ _isbest=value;}
			get{return _isbest;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Integral
		{
			set{ _integral=value;}
			get{return _integral;}
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
		public int? ReplyCount
		{
			set{ _replycount=value;}
			get{return _replycount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobileTele
		{
			set{ _mobiletele=value;}
			get{return _mobiletele;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReplyID
		{
			set{ _replyid=value;}
			get{return _replyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OriginalID
		{
			set{ _originalid=value;}
			get{return _originalid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string ReplyUserID
		{
			set{ _replyuserid=value;}
			get{return _replyuserid;}
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
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.UserID = cl.Contains("UserID") && dr["UserID"] != DBNull.Value ? dr["UserID"].ToString() : "";
            this.TypeID = cl.Contains("TypeID") && dr["TypeID"] != DBNull.Value ? Convert.ToInt32(dr["TypeID"]) : 0;
            this.Content = cl.Contains("Content") && dr["Content"] != DBNull.Value ? dr["Content"].ToString() : "";
            this.IsReply = cl.Contains("IsReply") && dr["IsReply"] != DBNull.Value ? dr["IsReply"].ToString() : "";
            this.IsBest = cl.Contains("IsBest") && dr["IsBest"] != DBNull.Value ? dr["IsBest"].ToString() : "";
            this.Status = cl.Contains("Status") && dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
            this.Integral = cl.Contains("Integral") && dr["Integral"] != DBNull.Value ? Convert.ToDecimal(dr["Integral"]) : 0;
            this.PraiseCount = cl.Contains("PraiseCount") && dr["PraiseCount"] != DBNull.Value ? Convert.ToInt32(dr["PraiseCount"]) : 0;
            this.ReplyCount = cl.Contains("ReplyCount") && dr["ReplyCount"] != DBNull.Value ? Convert.ToInt32(dr["ReplyCount"]) : 0;
            this.Name = cl.Contains("Name") && dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
            this.MobileTele = cl.Contains("MobileTele") && dr["MobileTele"] != DBNull.Value ? dr["MobileTele"].ToString() : "";
            this.ReplyID = cl.Contains("ReplyID") && dr["ReplyID"] != DBNull.Value ? Convert.ToInt32(dr["ReplyID"]) : 0;
            this.OriginalID = cl.Contains("OriginalID") && dr["OriginalID"] != DBNull.Value ? Convert.ToInt32(dr["OriginalID"]) : 0;
            this.ReplyUserID = cl.Contains("ReplyUserID") && dr["ReplyUserID"] != DBNull.Value ? dr["ReplyUserID"].ToString() : "";
            this.CreateDate = cl.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : DateTime.MinValue;
            this.OperateIP = cl.Contains("OperateIP") && dr["OperateIP"] != DBNull.Value ? dr["OperateIP"].ToString() : "";
            this.OperateID = cl.Contains("OperateID") && dr["OperateID"] != DBNull.Value ? dr["OperateID"].ToString() : "";
        }
		#endregion Model

	}
}

