using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KaoAKao.Entity;
using KaoAKao.Entity.Enum;
using KaoAKao.Business;

using Newtonsoft.Json.Linq;
using KaoAKao2._0.Web.Models;
using System.Drawing;

namespace KaoAKao2._0.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region view
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Course()
        {
            int pageSize = 5;
            int pageIndex = 1;
            int total = 0;
            int pages = 0;

            List<CourseEntity> courses = CourseBusiniss.GetCourses(string.Empty, string.Empty,
                CourseOrderBy.CreateDate, false, pageSize, pageIndex, out total, out pages);
            return View(courses);
        }

        public ActionResult Detail(string CourseID)
        {
            ViewBag.CourseID = CourseID;
            CourseEntity  course= CourseBusiniss.GetCourseByID(CourseID);
            //List<LessonEntity> lessons= CourseBusiniss.GetCourseLessons(CourseID);

            KaoAKao2._0.Web.Models.CourseDetail courseDetail = new CourseDetail();
            courseDetail.course = course;
            ViewBag.TeacherID = course.TeacherID;
            //courseDetail.lessons = lessons;

            return View(courseDetail);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        #endregion

        #region ajax
        #region 注册/登录
        /// <summary>
        /// 注册用户
        /// </summary>
        public ActionResult UserRegister(FormCollection paras)
        {
            string name = paras["UserName"];
            string pwd = paras["UserPwd"];
            string isEmail = paras["IsEmail"];
            string code = paras["code"];

            if (Session["code"].ToString() == code)
            {
                string email = string.Empty;
                string mobile = string.Empty;
                if (isEmail == "0")
                    mobile = name;
                else
                    email = name;

                if (!UserBusiness.IsExistUserName(name))
                {
                    int result = 0;
                    string resultdes = "";
                    string userID = UserBusiness.AddUsers(mobile, email, pwd, UserType.User, string.Empty, string.Empty, out result, out resultdes);
                    if (!string.IsNullOrEmpty(userID))
                    {
                        UserEntity user = UserBusiness.GetUserByUserID(userID);
                        if (user != null)
                        {
                            ResultObj.Add("result", 1);
                            Session["User"] = user;
                        }
                    }
                    else
                        ResultObj.Add("result", 0);
                }
                else
                    ResultObj.Add("result", 3);
            }
            else
                ResultObj.Add("result", 2);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        public ActionResult Validate()
        {
            UserEntity user = Session["User"] as UserEntity;
            if (user != null)
            {
                ResultObj.Add("result", 1);
                string userName = string.Empty;
                if (!string.IsNullOrEmpty(user.Email))
                    userName = user.Email;
                else if (!string.IsNullOrEmpty(user.MobileTele))
                    userName = user.MobileTele;
                else if (!string.IsNullOrEmpty(user.Name))
                    userName = user.Name;
                ResultObj.Add("userName", userName);
                ResultObj.Add("userID", user.UserID);
            }
            else
                ResultObj.Add("result", 0);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public ActionResult UserLogin(FormCollection paras)
        {
            string name = paras["Name"];
            string pwd = paras["Pwd"];

            UserEntity user = UserBusiness.UserLogin(name, pwd);
            if (user != null)
            {
                Session["User"] = user;
                ResultObj.Add("result", 1);
            }
            else
            {
                ResultObj.Add("result", 0);
            }
            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 用户登出
        /// </summary>
        public ActionResult Logout()
        {
            Session.Remove("User");
            if (Request.Cookies["passportInfo"] != null)
            {
                Response.Cookies["passportInfo"].Values.Clear();
                Response.Cookies["passportInfo"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("index", "home");
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        public void Code()
        {
            CreateCkeckCodeImage(GenerateCheckCode());
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateCkeckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
            {
                return;
            }

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 13.5)), 27);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(System.Drawing.Color.White);
                for (int i = 0; i < 2; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 14, FontStyle.Bold);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());

            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 生成随机的验证码数字
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                checkCode += code.ToString();
            }

            Session["code"] = checkCode;
            return checkCode;
        }

        #endregion

        #region 课程
        /// <summary>
        ///  获取课程列表
        /// </summary>
        public ActionResult GetCourses(FormCollection paras)
        {
            int pageSize=2;
            int pageIndex = int.Parse(paras["PageIndex"] ?? "1");
            int total=0;
            int pages=0;
            string keywords=paras["Keywords"]??string.Empty;
            string cID = paras["CID"] ?? string.Empty;
            int orderType =int.Parse( paras["OrderType"] ??"1");
            List<CourseEntity> courses = new List<CourseEntity>();

            courses = CourseBusiniss.GetCourses(cID, keywords, 
  CourseOrderBy.CreateDate, false, pageSize, pageIndex, out total, out pages);
            ResultObj.Add("result",1);
            ResultObj.Add("total", total);
            ResultObj.Add("pages", pages);
            ResultObj.Add("courses", courses);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  获取推荐课程列表
        /// </summary>
        public ActionResult GetGoodCourses()
        {
            int pageSize = 5;
            int pageIndex = 1;
            int total = 0;
            int pages = 0;

            List<CourseEntity> courses = CourseBusiniss.GetCourses(string.Empty, string.Empty,
                CourseOrderBy.CreateDate, false, pageSize, pageIndex, out total, out pages);

            ResultObj.Add("result", 1);
            ResultObj.Add("courses", courses);
            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  获取教师列表
        /// </summary>
        public ActionResult GetTeachers(FormCollection paras)
        {
            int pageSize = 16;
            int pageIndex = int.Parse(paras["PageIndex"] ?? "1");
            int total = 0;
            int pages = 0;
            string keywords = paras["Keywords"] ?? string.Empty;

            List<UserEntity> teachers =UserBusiness.GetUsers (keywords, pageSize, pageIndex, UserType.Teacher,out total, out pages);
            ResultObj.Add("result", 1);
            ResultObj.Add("total", total);
            ResultObj.Add("pages", pages);
            ResultObj.Add("teachers", teachers);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  获取课程分类
        /// </summary>
        public ActionResult GetCourseCategorys(FormCollection paras)
        {
            int pageSize =int.MaxValue;
            int pageIndex = 1;
            int total = 0;
            int pages = 0;
            string keywords = paras["Keywords"] ?? string.Empty;
            string pID = paras["PID"] ?? string.Empty;

            List<CourseCategoryEntity> categorys = CourseBusiniss.GetCourseCategorys(pID, keywords,
pageSize, pageIndex, out total, out pages);
            ResultObj.Add("result", 1);
            ResultObj.Add("total", total);
            ResultObj.Add("pages", pages);
            ResultObj.Add("categorys", categorys);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 课程章节
        /// <summary>
        /// 根据课程id获取课程章节列表
        /// </summary>
        public ActionResult GetLessonsByCid(FormCollection paras)
        {
            string cID = paras["CID"] ?? string.Empty;
            string tID = paras["TID"] ?? string.Empty;

            List<LessonEntity> resultLessons = new List<LessonEntity>();
            List<LessonEntity> lessons = CourseBusiniss.GetCourseLessons(cID);
            foreach (var lesson in lessons)
            {
                foreach (var clesson in lesson.ChildLessons)
                {
                    resultLessons.Add(clesson);
                }
            }
            ResultObj.Add("result", 1);
            ResultObj.Add("lessons", resultLessons);
            UserEntity teacher= UserBusiness.GetUserByUserID(tID);
            ResultObj.Add("teacher", teacher);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///对课程章节进行 点赞、喜欢、分享
        /// </summary>
        public ActionResult OperateLesson(FormCollection paras)
        {
            if (UserDetail != null)
            {
                string lID = paras["LID"] ?? string.Empty;
                string cID = paras["CID"] ?? string.Empty;
                int type = int.Parse(paras["OperateLessonType"] ?? "0");
                int result = 0;
                string resultDes = string.Empty;

                CourseBusiniss courseBusiniss = new CourseBusiniss();
                courseBusiniss.AddUserCourse(UserDetail.UserID, cID, lID,
                    (UserCourseType)type, string.Empty,UserDetail.UserID,
                    out result, out resultDes);

                ResultObj.Add("result", result > 0 ? 1 : 0);
            }
            else
                ResultObj.Add("result", -1);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取课程的评价或问题列表
        /// </summary>
        public ActionResult GetUserInteractions(FormCollection paras)
        {
            string cID = paras["CID"] ?? string.Empty;
            int type = int.Parse(paras["InteractiveType"] ?? "1");
            int pageSize = 10;
            int pageIndex = int.Parse(paras["PageIndex"] ?? "1");
            int total = 0;
            int pages = 0;
            string resultDes = string.Empty;

            List<UserInteraction> userInteractions = CourseBusiniss.GetUserInteractions(cID, (InteractiveType)type, pageSize, pageIndex, out total, out pages);
            ResultObj.Add("result", 1);
            ResultObj.Add("total", total);
            ResultObj.Add("pages", pages);
            ResultObj.Add("userInteractions", userInteractions);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据ID 获取评论回复、问题答案列表
        /// </summary>
        public ActionResult GetUserInteractionReplysByID(FormCollection paras)
        {
            int rID =int.Parse( paras["RID"] ??"0");
            int pageSize = 10;
            int pageIndex = int.Parse(paras["PageIndex"] ?? "1");
            int total = 0;
            int pages = 0;
            string resultDes = string.Empty;

            List<UserInteraction> userInteractions = CourseBusiniss.GetUserInteractionReplysByID(rID, pageSize, pageIndex, out total, out pages);
            ResultObj.Add("result", 1);
            ResultObj.Add("total", total);
            ResultObj.Add("pages", pages);
            ResultObj.Add("userInteractions", userInteractions);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///对课程进行评论或提出问答
        /// </summary>
        public ActionResult AddCourseInteraction(FormCollection paras)
        {
            if (UserDetail != null)
            {
                string cID = paras["CID"] ?? string.Empty;
                string content = paras["Content"] ?? string.Empty;
                int type = int.Parse(paras["InteractiveType"] ?? "1");
                int replyID = int.Parse(paras["ReplyID"] ?? "0");
                int result = 0;

                CourseBusiniss courseBusiniss = new CourseBusiniss();
                int id = courseBusiniss.AddCourseInteraction(UserDetail.UserID, cID, replyID, content,
                    (InteractiveType)type,0, string.Empty,
                    UserDetail.UserID, out result);

                ResultObj.Add("result", result > 0 ? 1 : 0);
                ResultObj.Add("replyID", id);
            }
            else
                ResultObj.Add("result", -1);

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region common

        #endregion


    }
}
