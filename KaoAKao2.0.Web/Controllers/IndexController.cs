using KaoAKao.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KaoAKao2._0.Web.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            int total = 0;
            int pages = 0;

            List<KaoAKao.Entity.CourseCategoryEntity> list = new List<KaoAKao.Entity.CourseCategoryEntity>();
            string table = "CourseCategory c left join CourseCategory p on c.PID=p.CategoryID";
            string columns = " c.*,p.CategoryName PName";
            StringBuilder build = new StringBuilder();
            build.Append(" c.Status <> 9 ");


            DataTable dt = CommonBusiness.GetPagerData(table, columns, build.ToString(), "c.ID", 1, 2, out total, out pages);

            foreach (DataRow dr in dt.Rows)
            {
                KaoAKao.Entity.CourseCategoryEntity category = new KaoAKao.Entity.CourseCategoryEntity();

                list.Add(category);
            }
            return View();
        }



    }
}
