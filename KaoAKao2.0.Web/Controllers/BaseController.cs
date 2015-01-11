using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KaoAKao.Entity;

namespace KaoAKao2._0.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Dictionary<string, object> ResultObj = new Dictionary<string, object>();

        protected UserEntity UserDetail
        {
            get {
                if (Session["User"] != null)
                {
                    return (UserEntity)Session["User"];
                }
                else return null;
            }
        }
    }

    
}
