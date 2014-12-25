/**  版本信息模板在安装目录下，可自行修改。
* ManagerRole.cs
*
* 功 能： N/A
* 类 名： ManagerRole
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/23 21:21:55   N/A    初版
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
	/// ManagerRole:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ManagerRoleEntity
	{
        public ManagerRoleEntity()
		{}
		#region Model
		private int _id;
		private string _managerid;
		private string _roleid;
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
		public string ManagerID
		{
			set{ _managerid=value;}
			get{return _managerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.ManagerID = cl.Contains("ManagerID") && dr["ManagerID"] != DBNull.Value ? dr["ManagerID"].ToString() : "";
            this.RoleID = cl.Contains("RoleID") && dr["RoleID"] != DBNull.Value ? dr["RoleID"].ToString() : "";
        }
		#endregion Model

	}
}

