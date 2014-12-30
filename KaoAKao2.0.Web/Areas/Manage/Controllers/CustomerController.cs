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

        public ActionResult CreateUserLevel()
        {
            return View();
        }

        public ActionResult UserLevel()
        {
            return View();
        }

        public ActionResult CreateTeacher()
        {
            return View();
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
                string id = KaoAKao.Business.UserBusiness.AddUsers(model.Name, model.MobileTele, model.Email, "", model.PhotoPath, KaoAKao.Entity.Enum.UserType.Teacher, model.KeyWords, model.Description, OperateIP, CurrentManager.Number);

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
        /// 添加教师
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JsonResult SaveUserLevel(string userLevel)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            KaoAKao.Entity.UserLevelEntity model = serializer.Deserialize<KaoAKao.Entity.UserLevelEntity>(userLevel);

            int result = 0;
            int id = KaoAKao.Business.UserBusiness.AddUserLevel(model.Level.Value, model.Name, model.Type, model.MinExp.Value, model.Discount.Value, model.ImgPath, model.Description, OperateIP, CurrentManager.Number, out result);

            JsonDictionary.Add("ID", result);

            return new JsonResult() { Data = JsonDictionary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion

    }
}
