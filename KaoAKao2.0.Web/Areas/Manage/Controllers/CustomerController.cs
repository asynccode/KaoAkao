using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult UserLevel()
        {
            return View();
        }

    }
}
