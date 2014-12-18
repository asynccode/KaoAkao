using System;
using System.Data;
namespace KaoAKao.Entity
{
	/// <summary>
	/// AreaCity:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AreaCityEntity
	{
		public AreaCityEntity()
		{}
		#region Model
		private int _id;
		private string _areacode="";
		private string _areaname="";
		private string _pareacode="";
		private int _arealevel=1;
		private string _country="";
		private string _province="";
		private string _city="";
		private string _county="";
		private string _postcode="";
		/// <summary>
        /// ID自增列
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// 地区编码
		/// </summary>
		public string AreaCode
		{
			set{ _areacode=value;}
			get{return _areacode;}
		}
		/// <summary>
        /// 地区名称
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
        /// 上级编码
		/// </summary>
		public string PAreaCode
		{
			set{ _pareacode=value;}
			get{return _pareacode;}
		}
		/// <summary>
        /// 地区级别(1:省份2:城市3:区县)
		/// </summary>
		public int AreaLevel
		{
			set{ _arealevel=value;}
			get{return _arealevel;}
		}
		/// <summary>
        /// 国家
		/// </summary>
		public string Country
		{
			set{ _country=value;}
			get{return _country;}
		}
		/// <summary>
        /// 省份
		/// </summary>
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
        /// 城市
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
        /// 区县
		/// </summary>
		public string County
		{
			set{ _county=value;}
			get{return _county;}
		}
		/// <summary>
        /// 邮政编码
		/// </summary>
		public string PostCode
		{
			set{ _postcode=value;}
			get{return _postcode;}
		}
		#endregion Model
        /// <summary>
        /// 数据填充
        /// </summary>
        /// <param name="dr">传入单行数据源</param>
        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.ID = cl.Contains("ID") ? Convert.ToInt32(dr["ID"]) : 0;
            this.AreaCode = cl.Contains("AreaCode") ? dr["AreaCode"].ToString() : "";
            this.AreaLevel = cl.Contains("AreaLevel") ? Convert.ToInt32(dr["AreaLevel"]) : 0;
            this.AreaName = cl.Contains("AreaName") ? dr["AreaName"].ToString() : "";
            this.City = cl.Contains("City") ? dr["City"].ToString() : "";
            this.Country = cl.Contains("Country") ? dr["Country"].ToString() : "";
            this.County = cl.Contains("County") ? dr["County"].ToString() : "";
            this.PostCode = cl.Contains("PostCode") ? dr["PostCode"].ToString() : "";
            this.PAreaCode = cl.Contains("PAreaCode") ? dr["PAreaCode"].ToString() : "";
            this.Province = cl.Contains("Province") ? dr["Province"].ToString() : "";
        }
	}
}

