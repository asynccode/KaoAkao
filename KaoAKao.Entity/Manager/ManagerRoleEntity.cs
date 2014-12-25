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
		private Guid _managerid;
		private Guid _roleid;
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
		/// <summary>
		/// 
		/// </summary>
		public Guid RoleID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		#endregion Model

	}
}

