/**  版本信息模板在安装目录下，可自行修改。
* Manager.cs
*
* 功 能： N/A
* 类 名： Manager
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:54   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
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
		private Guid _managerid;
		private string _loginname;
		private string _loginpwd;
		private string _name="";
		private string _email="";
		private string _mobile="";
		private string _tel="";
		private string _areacode="";
		private string _address="";
		private DateTime? _birthday= Convert.ToDateTime("1900-01-01");
		private int? _age=1;
		private int? _sex=0;
		private int? _education=0;
		private string _jobs="";
		private string _avatar;
		private Guid _departid;
		private Guid _pid;
		private string _allocation= "0";
		private string _description="";
		private DateTime? _createdate= DateTime.Now;
		private DateTime? _effectdate= Convert.ToDateTime("1900-01-01");
		private DateTime? _turnoverdate= Convert.ToDateTime("1900-01-01");
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
		public Guid ManagerID
		{
			set{ _managerid=value;}
			get{return _managerid;}
		}
        public string Number { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LoginName
		{
			set{ _loginname=value;}
			get{return _loginname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginPWD
		{
			set{ _loginpwd=value;}
			get{return _loginpwd;}
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
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaCode
		{
			set{ _areacode=value;}
			get{return _areacode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Education
		{
			set{ _education=value;}
			get{return _education;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Jobs
		{
			set{ _jobs=value;}
			get{return _jobs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Avatar
		{
			set{ _avatar=value;}
			get{return _avatar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid DepartID
		{
			set{ _departid=value;}
			get{return _departid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Allocation
		{
			set{ _allocation=value;}
			get{return _allocation;}
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
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectDate
		{
			set{ _effectdate=value;}
			get{return _effectdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TurnoverDate
		{
			set{ _turnoverdate=value;}
			get{return _turnoverdate;}
		}
		#endregion Model

	}
}

