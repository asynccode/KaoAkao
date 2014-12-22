using System;
using System.Data;
namespace KaoAKao.Entity
{
	/// <summary>
	/// Manager:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ManagerEntity
	{
        public ManagerEntity()
		{}
		#region Model
		private int _id;
		private string _number;
		private string _name;
		private string _loginpwd;
		private DateTime? _createdate= DateTime.Now;
		private DateTime? _lastlogindate= DateTime.Now;
        private int _islock = 0;
		private int? _roleid=0;
		private int? _deptid=0;
		private string _operatenum="";
		private string _operateip="";

		/// <summary>
		/// 
		/// </summary>
        public int AutoID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
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
		public string LoginPwd
		{
			set{ _loginpwd=value;}
			get{return _loginpwd;}
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
		public DateTime? LastLoginDate
		{
			set{ _lastlogindate=value;}
			get{return _lastlogindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeptID
		{
			set{ _deptid=value;}
			get{return _deptid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateNum
		{
			set{ _operatenum=value;}
			get{return _operatenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateIP
		{
			set{ _operateip=value;}
			get{return _operateip;}
		}
		#endregion Model


        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.AutoID = cl.Contains("AutoID") && dr["AutoID"] != DBNull.Value ? Convert.ToInt32(dr["AutoID"]) : 0;
            this.RoleID = cl.Contains("RoleID") && dr["RoleID"] != DBNull.Value ? Convert.ToInt32(dr["RoleID"]) : 0;
            this.DeptID = cl.Contains("DeptID") && dr["DeptID"] != DBNull.Value ? Convert.ToInt32(dr["DeptID"]) : 0;
            this.IsLock = cl.Contains("IsLock") && dr["IsLock"] != DBNull.Value ? Convert.ToInt32(dr["IsLock"]) : 0;
            this.Number = cl.Contains("Number") && dr["Number"] != DBNull.Value ? dr["Number"].ToString() : "";
            this.Name = cl.Contains("Name") && dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
            this.LoginPwd = cl.Contains("LoginPwd") && dr["LoginPwd"] != DBNull.Value ? dr["LoginPwd"].ToString() : "";
            this.OperateNum = cl.Contains("OperateNum") && dr["OperateNum"] != DBNull.Value ? dr["OperateNum"].ToString() : "";
            this.OperateIP = cl.Contains("OperateIP") && dr["OperateIP"] != DBNull.Value ? dr["OperateIP"].ToString() : "";
            this.CreateDate = cl.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : DateTime.MinValue;
        }
	}
}

