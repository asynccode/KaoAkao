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

namespace KaoAKao2._0.Web.Controllers
{

    public class AjaxController : Controller
    {
        JObject ResultObj = new JObject();

        #region 注册/登录
        /// <summary>
        /// 注册用户
        /// </summary>
        public ActionResult Register(FormCollection paras)
        {
            string name = paras["UserName"];
            string pwd = paras["UserPwd"];
            string isEmail = paras["IsEmail"];

            string email=string.Empty;
            string mobile=string.Empty;
            if(isEmail=="0")
                mobile=name;
            else
                email=name;
            string userID = UserBusiness.AddUsers(mobile, email, pwd, UserType.User,string.Empty,string.Empty);
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
                ResultObj.Add("result",0);
            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        public ActionResult Validate()
        {
            ResultObj = new JObject();
            UserEntity user = Session["User"] as UserEntity;
            if (user == null)
                ResultObj.Add("result", 0);
            else
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
            }

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public ActionResult Login(FormCollection paras)
        {
            string name=paras[""];
            string pwd = paras[""];

            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        
        #endregion

    }
}
