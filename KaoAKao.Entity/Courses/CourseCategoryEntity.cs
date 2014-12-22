using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KaoAKao.Entity
{
    /// <summary>
    /// 课程分类实体
    /// </summary>
    public class CourseCategoryEntity
    {
        public int AutoID { get; set; }

        public string CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string PID { get; set; }

        public string PName { get; set; }

        public string PIDList { get; set; }

        public string ImgURL { get; set; }

        public string KeyWords { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int IsHot { get; set; }

        public int Sort { get; set; }

        public void FillData(DataRow dr)
        {
            var cl = dr.Table.Columns;
            this.AutoID = cl.Contains("AutoID") && dr["AutoID"] != DBNull.Value ? Convert.ToInt32(dr["AutoID"]) : 0;
            this.CategoryID = cl.Contains("CategoryID") && dr["CategoryID"] != DBNull.Value ? dr["CategoryID"].ToString() : "";
            this.CategoryName = cl.Contains("CategoryName") && dr["CategoryName"] != DBNull.Value ? dr["CategoryName"].ToString() : "";
            this.PID = cl.Contains("PID") && dr["PID"] != DBNull.Value ? dr["PID"].ToString() : "";
            this.PName = cl.Contains("PName") && dr["PName"] != DBNull.Value ? dr["PName"].ToString() : "";
            this.PIDList = cl.Contains("PIDList") && dr["PIDList"] != DBNull.Value ? dr["PIDList"].ToString() : "";
            this.ImgURL = cl.Contains("CategoryImg") && dr["CategoryImg"] != DBNull.Value ? dr["CategoryImg"].ToString() : "";
            this.KeyWords = cl.Contains("KeyWords") && dr["KeyWords"] != DBNull.Value ? dr["KeyWords"].ToString() : "";
            this.Description = cl.Contains("Description") && dr["Description"] != DBNull.Value ? dr["Description"].ToString() : "";
            this.CreateDate = cl.Contains("CreateDate") && dr["CreateDate"] != DBNull.Value ? Convert.ToDateTime(dr["CreateDate"]) : DateTime.MinValue;
            this.IsHot = cl.Contains("IsHot") && dr["IsHot"] != DBNull.Value ? Convert.ToInt32(dr["IsHot"]) : 0;
            this.Sort = cl.Contains("Sort") && dr["Sort"] != DBNull.Value ? Convert.ToInt32(dr["Sort"]) : 0;
        }
    }
}
