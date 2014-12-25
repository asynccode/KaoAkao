/**  版本信息模板在安装目录下，可自行修改。
* Department.cs
*
* 功 能： N/A
* 类 名： Department
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
	/// Department:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DepartmentEntity
	{
        public DepartmentEntity()
		{}
		#region Model
		private int _id;
		private string _departid;
		private string _departname;
		private string _pid;
		private string _description="";
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
		public string DepartID
		{
			set{ _departid=value;}
			get{return _departid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepartName
		{
			set{ _departname=value;}
			get{return _departname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") && dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            this.DepartID = cl.Contains("DepartID") && dr["DepartID"] != DBNull.Value ? dr["DepartID"].ToString() : "";
            this.DepartName = cl.Contains("DepartName") && dr["DepartName"] != DBNull.Value ? dr["DepartName"].ToString() : "";
            this.PID = cl.Contains("PID") && dr["PID"] != DBNull.Value ? dr["PID"].ToString() : "";
            this.Description = cl.Contains("Description") && dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "";
        }
		#endregion Model

	}
}

