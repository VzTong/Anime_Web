using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Anime_Web.Areas.Admin
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        // GET: Admin/Check_login
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if(httpContext.Session["AdminName"] != null)
            {
                // Người dùng chưa đăng nhập, ko có quyền truy cập
                return true;
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Chuyển hướng người dùng đến trang đăng nhập
            if(filterContext.HttpContext.Session["AdminName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "AdminAccount", action = "AdminLogin" }));
            }
        }
    }
}