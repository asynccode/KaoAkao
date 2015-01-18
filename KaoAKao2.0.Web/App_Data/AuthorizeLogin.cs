using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KaoAKao2._0.Web.Models;

namespace KaoAKao2._0.Web.App_Code
{
    public class AuthorizeLogin : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = httpContext.Session["User"] as User;
            if (user == null)
                return false;
            else
                return true;

        }
    }
}