using KaoAKao.Business;
using KaoAKao.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace KaoAKao2._0.Web.Areas.Manage.Controllers
{
    public class CourseController : BaseController
    {
        //
        // GET: /Manage/Course/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCategory()
        {
            ViewBag.PList = CourseBusiniss.GetCourseCategorysByPID("");
            return View();
        }

        public ActionResult Categorys()
        {
            ViewBag.PList = CourseBusiniss.GetCourseCategorysByPID("");
            return View();
        }

        public ActionResult CategoryDetail(string id)
        {
            ViewBag.PList = CourseBusiniss.GetCourseCategorysByPID("");
            ViewBag.ID = id;
            return View();
        }

        public ActionResult CreateCourse()
        {
            ViewBag.LList = UserBusiness.GetUserLevelByType(UserType.User);
            ViewBag.TList = UserBusiness.GetTeachers();
            ViewBag.PList = CourseBusiniss.GetCourseCategorysByPID("");
            return View();
        }

        public ActionResult Courses()
        {
            ViewBag.PList = CourseBusiniss.GetCourseCategorysByPID("");
            return View();
        }

        public ActionResult CourseDetail(string id)
        {
            ViewBag.LList = UserBusiness.GetUserLevelByType(UserType.User);
            ViewBag.TList = UserBusiness.GetTeachers();
            ViewBag.PList = CourseBusiniss.GetCourseCategorysByPID("");
            ViewBag.ID = id;
            return View();
        }
        #region Ajax

        #region 课程分类
        /// <summary>
        /// 获取课程分类实体
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult GetCourseCategorysByPID(string pid)
        {
            var list = CourseBusiniss.GetCourseCategorysByPID(pid);
            JsonDictionary.Add("Items", list);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 获取课程分类列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="keywords"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonResult GetCourseCategorys(string pid, string keywords, int index)
        {
            int total = 0;
            int pages = 0;
            var list = CourseBusiniss.GetCourseCategorys(pid, keywords, PageSize, index, out total, out pages);

            JsonDictionary.Add("Total", total);
            JsonDictionary.Add("Pages", pages);
            JsonDictionary.Add("Items", list);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 添加课程分类
        /// </summary>
        /// <param name="categoy"></param>
        /// <returns></returns>
        public JsonResult SaveCourseCategoy(string categoy)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KaoAKao.Entity.CourseCategoryEntity model = serializer.Deserialize<KaoAKao.Entity.CourseCategoryEntity>(categoy);
            string CategoryID = string.Empty;
            if (string.IsNullOrEmpty(model.CategoryID))
            {
                CategoryID = new KaoAKao.Business.CourseBusiniss().AddCourseCategoy(model.CategoryName, model.PID, "", model.KeyWords, model.Description, OperateIP, CurrentManager.Number);
            }
            else
            {
                if (new KaoAKao.Business.CourseBusiniss().EditCourseCategoy(model.CategoryID, model.CategoryName, model.PID, "", model.KeyWords, model.Description, OperateIP, CurrentManager.Number))
                {
                    CategoryID = model.CategoryID;
                }
            }
            JsonDictionary.Add("ID", CategoryID);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        /// <summary>
        /// 获取课程分类实体
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult GetCourseCategoryByID(string cid)
        {
            var model = CourseBusiniss.GetCourseCategoryByID(cid);
            JsonDictionary.Add("Item", model);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 删除课程分类
        /// </summary>
        /// <param name="categoy"></param>
        /// <returns></returns>
        public JsonResult DeleteCourseCategoy(string categoyid)
        {
            bool bl = new KaoAKao.Business.CourseBusiniss().DeleteCourseCategoy(categoyid, OperateIP, CurrentManager.Number);

            JsonDictionary.Add("Status", bl);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        #endregion

        #region 课程


        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="keywords"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonResult GetCourses(string pid, string keywords, int index)
        {
            int total = 0;
            int pages = 0;
            var list = CourseBusiniss.GetCourses(pid, keywords, PageSize, index, out total, out pages);

            JsonDictionary.Add("Total", total);
            JsonDictionary.Add("Pages", pages);
            JsonDictionary.Add("Items", list);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="categoy"></param>
        /// <returns></returns>
        public JsonResult SaveCourse(string course)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KaoAKao.Entity.CourseEntity model = serializer.Deserialize<KaoAKao.Entity.CourseEntity>(course);
            string CourseID = string.Empty;
            if (string.IsNullOrEmpty(model.CourseID))
            {
                CourseID = new KaoAKao.Business.CourseBusiniss().AddCourse(model.CourseName, model.CategoryID, model.ImgURL, 0, model.TeacherID, model.IsHot.Value, model.LimitLevel.Value, model.Keywords, model.Description, OperateIP, CurrentManager.Number);
            }
            else
            {
                if (new KaoAKao.Business.CourseBusiniss().EditCourse(model.CourseID, model.CourseName, model.CategoryID, model.ImgURL, 0, model.TeacherID, model.IsHot.Value, model.LimitLevel.Value, model.Keywords, model.Description, OperateIP, CurrentManager.Number))
                {
                    CourseID = model.CourseID;
                }
            }
            JsonDictionary.Add("ID", CourseID);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        /// <summary>
        /// 获取课程实体
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult GetCourseByID(string courseid)
        {
            var model = CourseBusiniss.GetCourseByID(courseid);
            JsonDictionary.Add("Item", model);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="categoy"></param>
        /// <returns></returns>
        public JsonResult DeleteCourse(string courseid)
        {
            bool bl = new KaoAKao.Business.CourseBusiniss().DeleteCourse(courseid, OperateIP, CurrentManager.Number);

            JsonDictionary.Add("Status", bl);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        #endregion

        #endregion
    }
}
