/**  版本信息模板在安装目录下，可自行修改。
* Permissions.cs
*
* 功 能： N/A
* 类 名： Permissions
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:56   N/A    初版
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
	/// Permissions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PermissionEntity
	{
        public PermissionEntity()
		{}
		#region Model
		private int _id;
		private string _permissioncode;
		private string _name;
		private string _pcode;
		private string _area="";
		private string _controller="";
		private string _action="";
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
		public string PermissionCode
		{
			set{ _permissioncode=value;}
			get{return _permissioncode;}
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
		public string PCode
		{
			set{ _pcode=value;}
			get{return _pcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Controller
		{
			set{ _controller=value;}
			get{return _controller;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Action
		{
			set{ _action=value;}
			get{return _action;}
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
            this.PermissionCode = cl.Contains("PermissionCode") && dr["PermissionCode"] != DBNull.Value ? dr["PermissionCode"].ToString() : "";
            this.Name = cl.Contains("Name") && dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
            this.PCode = cl.Contains("PCode") && dr["PCode"] != DBNull.Value ? dr["PCode"].ToString() : "";
            this.Area = cl.Contains("Area") && dr["Area"] != DBNull.Value ? dr["Area"].ToString() : "";
            this.Status = cl.Contains("Status") && dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
            this.Controller = cl.Contains("Controller") && dr["Controller"] != DBNull.Value ? dr["Controller"].ToString() : "";
            this.Action = cl.Contains("Action") && dr["Action"] != DBNull.Value ? dr["Action"].ToString() : "";
        }
		#endregion Model

	}
}

