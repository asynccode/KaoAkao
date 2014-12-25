/**  版本信息模板在安装目录下，可自行修改。
* Users.cs
*
* 功 能： N/A
* 类 名： Users
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:58   N/A    初版
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
	/// Users:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserEntity
	{
        public UserEntity()
		{}
		#region Model
		private int _id;
		private Guid _userid;
		private string _username;
		private string _name="";
		private string _petname="";
		private string _loginpwd="";
		private string _securitypwd="";
		private int? _levelid=0;
		private int? _usertype=0;
		private int? _degree;
		private string _hometele="";
		private string _mobiletele="";
		private string _email="";
		private int? _sex;
		private DateTime? _birthday= Convert.ToDateTime("1900-01-01");
		private string _areacode="";
		private string _address="";
		private string _photopath="";
		private int? _status=0;
		private string _question="";
		private string _answer="";
		private string _postcode="";
		private string _papertypecode="";
		private string _papernumber="";
		private int? _integralin=0;
		private int? _integralout=0;
		private int? _growvalue=0;
		private int? _expvalue=0;
		private DateTime? _registerdate= DateTime.Now;
		private DateTime? _lastlogindate= DateTime.Now;
		private int? _pwderrortimes=0;
		private int? _logintimes=0;
		private string _keywords="";
		private string _description="";
		private DateTime? _lastoperatedate= DateTime.Now;
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
		public Guid UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
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
		public string PetName
		{
			set{ _petname=value;}
			get{return _petname;}
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
		public string SecurityPwd
		{
			set{ _securitypwd=value;}
			get{return _securitypwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LevelID
		{
			set{ _levelid=value;}
			get{return _levelid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Degree
		{
			set{ _degree=value;}
			get{return _degree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HomeTele
		{
			set{ _hometele=value;}
			get{return _hometele;}
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
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
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
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
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
		public string PhotoPath
		{
			set{ _photopath=value;}
			get{return _photopath;}
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
		public string Question
		{
			set{ _question=value;}
			get{return _question;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PostCode
		{
			set{ _postcode=value;}
			get{return _postcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaperTypeCode
		{
			set{ _papertypecode=value;}
			get{return _papertypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaperNumber
		{
			set{ _papernumber=value;}
			get{return _papernumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IntegralIn
		{
			set{ _integralin=value;}
			get{return _integralin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IntegralOut
		{
			set{ _integralout=value;}
			get{return _integralout;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GrowValue
		{
			set{ _growvalue=value;}
			get{return _growvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ExpValue
		{
			set{ _expvalue=value;}
			get{return _expvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegisterDate
		{
			set{ _registerdate=value;}
			get{return _registerdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastloginDate
		{
			set{ _lastlogindate=value;}
			get{return _lastlogindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PwdErrorTimes
		{
			set{ _pwderrortimes=value;}
			get{return _pwderrortimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LoginTimes
		{
			set{ _logintimes=value;}
			get{return _logintimes;}
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
		public DateTime? LastOperateDate
		{
			set{ _lastoperatedate=value;}
			get{return _lastoperatedate;}
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

	}
}

