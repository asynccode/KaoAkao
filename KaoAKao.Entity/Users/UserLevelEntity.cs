/**  版本信息模板在安装目录下，可自行修改。
* UserLevel.cs
*
* 功 能： N/A
* 类 名： UserLevel
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
	/// UserLevel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserLevelEntity
	{
        public UserLevelEntity()
		{}
		#region Model
		private int _id;
		private int? _level;
		private string _name="";
		private int? _type=1;
		private int? _minexp=0;
		private decimal? _discount=1M;
		private string _imgpath="";
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
		public int? Level
		{
			set{ _level=value;}
			get{return _level;}
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
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MinExp
		{
			set{ _minexp=value;}
			get{return _minexp;}
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
		public string ImgPath
		{
			set{ _imgpath=value;}
			get{return _imgpath;}
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

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.Level = cl.Contains("Level") && dr["Level"] != DBNull.Value ? Convert.ToInt32(dr["Level"]) : 0;
            this.Name = cl.Contains("Name") && dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
            this.Type = cl.Contains("Type") && dr["Type"] != DBNull.Value ? Convert.ToInt32(dr["Type"]) : 0;
            this.MinExp = cl.Contains("MinExp") && dr["MinExp"] != DBNull.Value ? Convert.ToInt32(dr["MinExp"]) : 0;
            this.Discount = cl.Contains("Discount") && dr["Discount"] != DBNull.Value ? Convert.ToDecimal(dr["Discount"]) : 0;
            this.ImgPath = cl.Contains("ImgPath") && dr["ImgPath"] != DBNull.Value ? dr["ImgPath"].ToString() : "";
            this.Description = cl.Contains("Description") && dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "";
            this.LastOperateDate = cl.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : DateTime.MinValue;
            this.OperateIP = cl.Contains("OperateIP") && dr["OperateIP"] != DBNull.Value ? dr["OperateIP"].ToString() : "";
            this.OperateID = cl.Contains("OperateID") && dr["OperateID"] != DBNull.Value ? dr["OperateID"].ToString() : "";
        }
		#endregion Model

	}
}

