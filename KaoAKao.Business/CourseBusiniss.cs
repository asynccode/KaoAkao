using KaoAKao.DAL;
using KaoAKao.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KaoAKao.Business
{
    public class CourseBusiniss
    {

        #region 生成课程分类编码
        public static object SingLockCategoryCode = new object();
        public static string CategoryDay = string.Empty;
        public static int CategoryCount = 1;
        /// <summary>
        /// 获取课程分类编码
        /// </summary>
        /// <returns></returns>
        public static string GetCategoryCode()
        {
            lock (SingLockCategoryCode)
            {
                string code = string.Empty;
                string now = DateTime.Now.ToString("yyMMdd");
                if (!CategoryDay.Equals(now))
                {
                    CategoryDay = now;
                    CategoryCount = 1;
                }
                else
                {
                    CategoryCount++;
                }
                code = CategoryDay + CategoryCount.ToString("0000");
                return code;
            }
        }
        #endregion

        #region 查询

        /// <summary>
        /// 获取课程分类列表(分页)
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="keywords"></param>
        /// <param name="pageSize"></param>
        /// <param name="index"></param>
        /// <param name="total"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public static List<Entity.CourseCategoryEntity> GetCourseCategorys(string pid, string keywords, int pageSize, int index, out int total, out int pages)
        {
            List<Entity.CourseCategoryEntity> list = new List<Entity.CourseCategoryEntity>();
            string table = "CourseCategory c left join CourseCategory p on c.PID=p.CategoryID";
            string columns = " c.*,p.CategoryName PName";
            StringBuilder build = new StringBuilder();
            build.Append(" c.Status <> 9 ");
            if (pid != "-1")
            {
                build.Append(" and c.PID='" + pid + "'");
            }
            if (keywords != "")
            {
                build.Append(" and (c.CategoryName like '%" + keywords + "%' or p.CategoryName like '%" + keywords + "%')");
            }

            DataTable dt = CommonBusiness.GetPagerData(table, columns, build.ToString(), "c.ID", pageSize, index, out total, out pages);

            foreach (DataRow dr in dt.Rows)
            {
                CourseCategoryEntity category = new CourseCategoryEntity();
                category.FillData(dr);
                list.Add(category);
            }

            return list;
        }

        /// <summary>
        /// 根据PID获取课程分类列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static List<Entity.CourseCategoryEntity> GetCourseCategorysByPID(string pid)
        {
            List<Entity.CourseCategoryEntity> list = new List<Entity.CourseCategoryEntity>();

            DataTable dt = new DAL.CourseDAL().GetCourseCategorysByPID(pid);

            foreach (DataRow dr in dt.Rows)
            {
                CourseCategoryEntity category = new CourseCategoryEntity();
                category.FillData(dr);
                list.Add(category);
            }

            return list;
        }

        /// <summary>
        /// 根据ID获取课程分类实体
        /// </summary>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        public static Entity.CourseCategoryEntity GetCourseCategoryByID(string categoryid)
        {
            Entity.CourseCategoryEntity model = new CourseCategoryEntity();
            DataTable dt = new DAL.CourseDAL().GetCourseCategoryByID(categoryid);
            if (dt.Rows.Count > 0)
            {
                model.FillData(dt.Rows[0]);
            }
            return model;
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加课程分类
        /// </summary>
        /// <param name="categoryName">课程名</param>
        /// <param name="pid">上级ID</param>
        /// <param name="imgURL">图片</param>
        /// <param name="keyWords">关键词</param>
        /// <param name="desc">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public string AddCourseCategoy(string categoryName, string pid, string imgURL, string keyWords, string desc, string operateIP, string operateID)
        {
            string categoryid = GetCategoryCode();

            //if (imgURL != "/modules/images/default.png")
            //{
            //    if (imgURL.IndexOf("?") > 0)
            //    {
            //        imgURL = imgURL.Substring(0, imgURL.IndexOf("?"));
            //    }
            //    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(imgURL));
            //    imgURL = "/Content/upload_images/" + file.Name;
            //    file.MoveTo(HttpContext.Current.Server.MapPath(imgURL));
            //}

            if (new CourseDAL().AddCourseCategoy(categoryid, categoryName, pid, imgURL, keyWords, desc, operateIP, operateID))
            {
                return categoryid;
            }
            return string.Empty;
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑课程分类
        /// </summary>
        /// <param name="categoryName">课程分类ID</param>
        /// <param name="categoryName">课程名</param>
        /// <param name="pid">上级ID</param>
        /// <param name="imgURL">图片</param>
        /// <param name="keyWords">关键词</param>
        /// <param name="desc">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool EditCourseCategoy(string categoryid, string categoryName, string pid, string imgURL, string keyWords, string desc, string operateIP, string operateID)
        {
            return new CourseDAL().EditCourseCategoy(categoryid, categoryName, pid, imgURL, keyWords, desc, operateIP, operateID);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除课程分类
        /// </summary>
        /// <param name="categoryName">课程分类ID</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool DeleteCourseCategoy(string categoryid, string operateIP, string operateID)
        {
            return new CourseDAL().DeleteCourseCategoy(categoryid, operateIP, operateID);
        }

        #endregion
    }
}
