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


        /// <summary>
        /// 获取课程列表(分页)
        /// </summary>
        /// <param name="pid">上级ID</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="index">页码</param>
        /// <param name="total">返回总记录数</param>
        /// <param name="pages">返回总页码</param>
        /// <returns></returns>
        public static List<Entity.CourseEntity> GetCourses(string pid, string keywords, int pageSize, int index, out int total, out int pages)
        {
            List<Entity.CourseEntity> list = new List<Entity.CourseEntity>();
            string table = "Courses c left join CourseCategory p on c.CategoryID=p.CategoryID";
            string columns = " c.*,p.CategoryName CName";
            StringBuilder build = new StringBuilder();
            build.Append(" c.Status!= 9 and p.Status!= 9");
            if (pid != "-1" && pid != "")
            {
                build.Append(" and (c.CategoryID='" + pid + "' or p.PID='" + pid + "') ");
            }
            if (keywords != "")
            {
                build.Append(" and (c.CourseName like '%" + keywords + "%' or p.CategoryName like '%" + keywords + "%')");
            }

            DataTable dt = CommonBusiness.GetPagerData(table, columns, build.ToString(), "c.AutoID", pageSize, index, out total, out pages);

            foreach (DataRow dr in dt.Rows)
            {
                CourseEntity model = new CourseEntity();
                model.FillData(dr);
                list.Add(model);
            }

            return list;
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

            if (new CourseDAL().AddCourseCategoy(categoryid, categoryName, pid, imgURL, keyWords, desc, operateIP, operateID))
            {
                return categoryid;
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="courseName">课程名</param>
        /// <param name="cid">分类ID</param>
        /// <param name="courseImg">图片</param>
        /// <param name="price">单价</param>
        /// <param name="teacherid">教师ID</param>
        /// <param name="keyWords">关键词</param>
        /// <param name="desc">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public string AddCourse(string courseName, string cid, string imgURl, double price, string teacherid, int isHot ,int limitLevel,string keyWords, string desc, string operateIP, string operateID)
        {
            if (!string.IsNullOrEmpty(imgURl) && imgURl != "/modules/images/default.png")
            {
                if (imgURl.IndexOf("?") > 0)
                {
                    imgURl = imgURl.Substring(0, imgURl.IndexOf("?"));
                }
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(imgURl));
                imgURl = "/Content/upload_images/" + file.Name;
                file.MoveTo(HttpContext.Current.Server.MapPath(imgURl));
            }
            object obj = new CourseDAL().AddCourse(courseName, cid, imgURl, price, teacherid, isHot,limitLevel, keyWords, desc, operateIP, operateID);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
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
