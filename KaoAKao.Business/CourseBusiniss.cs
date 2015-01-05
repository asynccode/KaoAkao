﻿using KaoAKao.DAL;
using KaoAKao.Entity;
using KaoAKao.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                    if (string.IsNullOrEmpty(CategoryDay))
                    {
                        CategoryCount = Convert.ToInt32(CommonBusiness.GetColumnValue("CourseCategory", "count(0)", " and CategoryID like '" + now + "%'"));
                    }
                    CategoryDay = now;

                    CategoryCount++;
                }
                else
                {
                    if (CategoryCount <= 1)
                    {
                        CategoryCount = Convert.ToInt32(CommonBusiness.GetColumnValue("CourseCategory", "count(0)", " and CategoryID like '" + CategoryDay + "%'"));
                    }
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
        /// <param name="pid">上级分类ID</param>
        /// <param name="keywords">关键词</param>
        /// <param name="pageSize">页size</param>
        /// <param name="index">页码</param>
        /// <param name="total">返回总记录数</param>
        /// <param name="pages">返回总页数</param>
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
        /// <param name="pid">上级分类ID</param>
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
        /// <param name="categoryid">课程分类ID</param>
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
        /// <param name="keywords">关键词</param>
        /// <param name="pageSize">页size</param>
        /// <param name="index">页码</param>
        /// <param name="total">返回总记录数</param>
        /// <param name="pages">返回总页数</param
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

            DataTable dt = CommonBusiness.GetPagerData(table, columns, build.ToString(), "c.ID", pageSize, index, out total, out pages);

            foreach (DataRow dr in dt.Rows)
            {
                CourseEntity model = new CourseEntity();
                model.FillData(dr);
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 根据ID获取课程实体
        /// </summary>
        /// <param name="courseid">课程ID</param>
        /// <returns></returns>
        public static Entity.CourseEntity GetCourseByID(string courseid)
        {
            Entity.CourseEntity model = new CourseEntity();
            DataTable dt = new DAL.CourseDAL().GetCourseByID(courseid);
            if (dt.Rows.Count > 0)
            {
                model.FillData(dt.Rows[0]);
            }
            return model;
        }

        /// <summary>
        /// 根据课程ID获取章节列表
        /// </summary>
        /// <param name="courseid">课程ID</param>
        /// <returns></returns>
        public static List<Entity.LessonEntity> GetCourseLessons(string courseid)
        {
            DataTable dt = new CourseDAL().GetCourseLessons(courseid);

            List<Entity.LessonEntity> list = new List<Entity.LessonEntity>();
            foreach (DataRow dr in dt.Select("PID=''"))
            {
                LessonEntity model = new LessonEntity();
                model.FillData(dr);
                List<Entity.LessonEntity> clist = new List<Entity.LessonEntity>();
                foreach (DataRow cdr in dt.Select("PID='" + model.LessonID + "'"))
                {
                    LessonEntity cmodel = new LessonEntity();
                    cmodel.FillData(cdr);

                    clist.Add(cmodel);
                }
                model.ChildLessons = clist;
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 根据课程ID获取章节列表
        /// </summary>
        /// <param name="courseid">课程ID</param>
        /// <param name="pid">上级章节ID</param>
        /// <returns></returns>
        public static List<Entity.LessonEntity> GetCourseLessons(string courseid, string pid)
        {
            DataTable dt = new CourseDAL().GetCourseLessons(courseid, pid);

            List<Entity.LessonEntity> list = new List<Entity.LessonEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                LessonEntity model = new LessonEntity();
                model.FillData(dr);
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 根据ID获取章节实体
        /// </summary>
        /// <param name="lessonid">章节ID</param>
        /// <returns></returns>
        public static Entity.LessonEntity GetCourseLessonByID(string lessonid)
        {
            Entity.LessonEntity model = new LessonEntity();
            DataTable dt = new DAL.CourseDAL().GetCourseLessonByID(lessonid);
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


        /// <summary>
        /// 添加课程章节
        /// </summary>
        /// <param name="lessonName">名称</param>
        /// <param name="courseID">课程</param>
        /// <param name="pid">上级ID</param>
        /// <param name="lessonImg">图片</param>
        /// <param name="keyWords">关键词</param>
        /// <param name="description">描述</param>
        /// <param name="radioURL">视频ID</param>
        /// <param name="radioSize">视频大小</param>
        /// <param name="sort">排序</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public string AddCourseLesson(string lessonName, string courseID, string pid, string keywords, string description, string radioURL, string radioSize, int sort, string operateIP, string operateID)
        {
            object obj = new CourseDAL().AddCourseLesson(lessonName, courseID, pid, keywords, description, radioURL.Trim(), radioSize, sort, operateIP, operateID);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加收藏、点赞、分享等
        /// </summary>
        /// <param name="userid">会员ID</param>
        /// <param name="courseid">课程ID</param>
        /// <param name="lessonid">章节ID</param>
        /// <param name="type">UserCourseType 类型</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <param name="result">返回状态 1成功 0不成功</param>
        /// <param name="resultdes">返回字符串</param>
        /// <returns></returns>
        public string AddUserCourse(string userid, string courseid, string lessonid, UserCourseType type, string operateIP, string operateID, out int result, out string resultdes)
        {
            object obj = new CourseDAL().AddUserCourse(userid, courseid, lessonid, (int)type, operateIP, operateID, out result, out resultdes);
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

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="courseName">课程名</param>
        /// <param name="cid">分类ID</param>
        /// <param name="courseImg">图片</param>
        /// <param name="price">单价</param>
        /// <param name="teacherid">教师ID</param>
        /// <param name="isHot">是否推荐 1推荐</param>
        /// <param name="keyWords">关键词</param>
        /// <param name="desc">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool EditCourse(string courseid, string courseName, string cid, string imgURl, double price, string teacherid, int isHot, int limitLevel, string keyWords, string desc, string operateIP, string operateID)
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
            return new CourseDAL().EditCourse(courseid, courseName, cid, imgURl, price, teacherid, isHot, limitLevel, keyWords, desc, operateIP, operateID);
        }

        /// <summary>
        /// 编辑课程章节
        /// </summary>
        /// <param name="lessonName">名称</param>
        /// <param name="courseID">课程</param>
        /// <param name="pid">上级ID</param>
        /// <param name="lessonImg">图片</param>
        /// <param name="keyWords">关键词</param>
        /// <param name="description">描述</param>
        /// <param name="videoURL">视频ID</param>
        /// <param name="videoSize">视频大小</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool EditCourseLesson(string lessonID, string lessonName, string courseID, string pid, string keyWords, string description, string videoURL, string videoSize, int sort, string operateIP, string operateID)
        {
            bool bl = new CourseDAL().EditCourseLesson(lessonID, lessonName, courseID, pid, keyWords, description, videoURL.Trim(), videoSize, sort, operateIP, operateID);
            return bl;
        }

        /// <summary>
        /// 编辑课程章节排序
        /// </summary>
        /// <param name="lessonid">章节ID</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public bool EditLessonSort(string lessonid, int sort)
        {
            return CommonBusiness.updateValue("Lessons", "Sort", sort.ToString(), " LessonID='" + lessonid + "'");
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

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="categoryName">课程ID</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool DeleteCourse(string courseid, string operateIP, string operateID)
        {
            return new CourseDAL().DeleteCourse(courseid, operateIP, operateID);
        }


        /// <summary>
        /// 删除课程章节
        /// </summary>
        /// <param name="lessonid">课程章节ID</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool DeleteCourseLessons(string lessonid, string operateIP, string operateID)
        {
            return new CourseDAL().DeleteCourseLessons(lessonid, operateIP, operateID);
        }
        #endregion
    }
}
