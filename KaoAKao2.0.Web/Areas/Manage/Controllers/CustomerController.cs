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
    public class CustomerController : BaseController
    {
        //
        // GET: /Manage/Customer/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LevelDetail(string id)
        {
            ViewBag.ID = id;
            return View();
        }

        public ActionResult UserLevel()
        {
            ViewBag.Type = (int)UserType.User;
            return View();
        }

        public ActionResult TeacherLevel()
        {
            ViewBag.Type = (int)UserType.Teacher;
            return View("UserLevel");
        }

        public ActionResult CreateTeacher()
        {
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Type = "0";
            return View("Teachers");
        }
        public ActionResult Teachers()
        {
            ViewBag.Type = "1";
            return View();
        }

        public ActionResult TeacherDetail(string id)
        {
            ViewBag.ID = id;
            return View();
        }


        #region 教师 AJAX


        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonResult GetUsers(string keywords, int index, int type)
        {
            int total = 0;
            int pages = 0;
            var list = UserBusiness.GetUsers(keywords, PageSize, index, (UserType)type, out total, out pages);

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
        /// 添加教师
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult AddTeacher(string teacher)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KaoAKao.Entity.UserEntity model = serializer.Deserialize<KaoAKao.Entity.UserEntity>(teacher);
            if (UserBusiness.IsExistUserName(model.MobileTele))
            {
                JsonDictionary.Add("ID", string.Empty);
                JsonDictionary.Add("Err", "手机号码已存在！");
            }
            else if (UserBusiness.IsExistUserName(model.Email))
            {
                JsonDictionary.Add("ID", string.Empty);
                JsonDictionary.Add("Err", "邮箱已存在！");
            }
            else
            {
                int result = 0;
                string resultdes = "";
                string id = KaoAKao.Business.UserBusiness.AddUsers(model.Name, model.MobileTele, model.Email, "", model.PhotoPath, KaoAKao.Entity.Enum.UserType.Teacher, model.KeyWords, model.Description, OperateIP, CurrentManager.Number,out result,out resultdes);

                JsonDictionary.Add("ID", id);
            }

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// 获取会员详细信息
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonResult GetUserByUserID(string userid)
        {

            var model = UserBusiness.GetUserByUserID(userid);

            JsonDictionary.Add("Item", model);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 编辑教师
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult EditUsers(string user)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KaoAKao.Entity.UserEntity model = serializer.Deserialize<KaoAKao.Entity.UserEntity>(user);
            bool bl = false;
            if (UserBusiness.IsExistUserName(model.UserID, model.MobileTele))
            {
                JsonDictionary.Add("ID", string.Empty);
                JsonDictionary.Add("Err", "手机号码已存在！");
            }
            else if (UserBusiness.IsExistUserName(model.UserID, model.Email))
            {
                JsonDictionary.Add("ID", string.Empty);
                JsonDictionary.Add("Err", "邮箱已存在！");
            }
            else
            {
                bl = new KaoAKao.Business.UserBusiness().EditTeacher(model.UserID, "", model.Name, model.MobileTele, model.Email, model.PhotoPath, model.KeyWords, model.Description, OperateIP, CurrentManager.Number);

            }
            JsonDictionary.Add("Status", bl);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// 删除会员、教师
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult DeleteUser(string userid)
        {
            bool bl = new KaoAKao.Business.UserBusiness().DeleteUser(userid, OperateIP, CurrentManager.Number);

            JsonDictionary.Add("Status", bl);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

        #region 会员等级 AJAX

        /// <summary>
        /// 保存等级
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult SaveUserLevel(string userLevel)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KaoAKao.Entity.UserLevelEntity model = serializer.Deserialize<KaoAKao.Entity.UserLevelEntity>(userLevel);

            bool bl = new KaoAKao.Business.UserBusiness().EditUserLevel(model.ID, model.Name, model.ImgPath, model.Discount.Value, model.Description, OperateIP, CurrentManager.Number);

            JsonDictionary.Add("Status", bl);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// 获取等级列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult GetUserLevels(int type)
        {
            var list = KaoAKao.Business.UserBusiness.GetUserLevelByType((UserType)type);

            JsonDictionary.Add("Items", list);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// 获取等级详情
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult GetUserLevelByID(int id)
        {
            var model = KaoAKao.Business.UserBusiness.GetUserLevelByID(id);

            JsonDictionary.Add("Item", model);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

    }
}
