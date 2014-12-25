using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            string name = paras[""];
            return Json(ResultObj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        public ActionResult Validate()
        {
            User user =Session["User"] as User;
            if (user == null)
                ResultObj.Add("result", 0);
            else
                ResultObj.Add("result", 1);

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
