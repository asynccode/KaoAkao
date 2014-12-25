﻿/**  版本信息模板在安装目录下，可自行修改。
* RolePermissions.cs
*
* 功 能： N/A
* 类 名： RolePermissions
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
	/// RolePermissions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RolePermissionEntity
	{
        public RolePermissionEntity()
		{}
		#region Model
		private int _id;
		private string _roleid;
		private string _permissioncode;
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
		public string RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PermissionCode
		{
			set{ _permissioncode=value;}
			get{return _permissioncode;}
		}

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.RoleID = cl.Contains("RoleID") && dr["RoleID"] != DBNull.Value ? dr["RoleID"].ToString() : "";
            this.PermissionCode = cl.Contains("PermissionCode") && dr["PermissionCode"] != DBNull.Value ? dr["PermissionCode"].ToString() : "";
        }
		#endregion Model

	}
}

