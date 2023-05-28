using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;

namespace Anime_Web.Controllers
{
    public class AccountController : Controller
    {
        WEB_Anime_ASPEntities _db = new WEB_Anime_ASPEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string pass) 
        {
            if (ModelState.IsValid)
            {
                var check = _db.Accounts.Where(x => x.password.Equals(pass)).ToList();
                if(check.Count == 0)
                {
                    return RedirectToAction("Login");
                }
                var data1 = _db.Accounts.Where(x => x.username.Equals(username) & x.password.Equals(pass) );
                var data2 = _db.Accounts.Where(x => x.email.Equals(username) & x.password.Equals(pass));
                if((data1.Count() != 0) )
                {
                    Session["username"] = data1.FirstOrDefault().username;
                    Session["id"] = data1.FirstOrDefault().id;

                    return RedirectToAction("Index", "Home");
                }
                else if( (data2.Count() != 0))
                {
                    Session["username"] = data2.FirstOrDefault().email;
                    Session["id"] = data1.FirstOrDefault().id;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.mess_err = "thông tin tài khoản or mật khảu không chính xacs";
                    return RedirectToAction("Login");
                }
            }


            return View();
        }
    }
}