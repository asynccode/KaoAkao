using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaoAKao.Entity;

namespace KaoAKao2._0.Web.Areas.Manage.Controllers
{
    public class BaseController : Controller
    {
        protected Dictionary<string, object> JsonDictionary = new Dictionary<string, object>();

        protected int PageSize = 20;

        /// <summary>
        /// 当前登录IP
        /// </summary>
        protected string OperateIP = System.Web.HttpContext.Current.Request.UserHostAddress;

        /// <summary>
        /// 当前登录管理员信息
        /// </summary>
        protected ManagerEntity CurrentManager = new ManagerEntity() { Number = "admin" };

    }
}
