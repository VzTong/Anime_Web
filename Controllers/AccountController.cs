using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;
using System.Security.Cryptography;
using System.Text;

namespace Anime_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly WEB_Anime_ASPEntities1 _db = new WEB_Anime_ASPEntities1();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountM _User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check user_name 
                    var check = _db.Accounts.FirstOrDefault(s => s.username == _User.username);

                    if (check == null)
                    {
                        _User.password = GetMD5(_User.password);
                        _db.Configuration.ValidateOnSaveEnabled = false;

                        var account = new Account
                        {
                            email = _User.email,
                            username = _User.username,
                            password = _User.password,
                            ischeck = 0
                        };
                        _db.Accounts.Add(account);
                        _db.SaveChanges();

                        ModelState.Clear();
                        ViewBag.Message = account.username + " Successfully registerd.";
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.error = "Username already exists";
                        return View();
                    }
                }
            }

            catch (Exception)
            {
                Console.WriteLine("Something went wrong.");
            }
            return View();

        }

        //-------------------------------//

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                //var f_pass = getmd5(pass);
                var check = _db.Accounts.Where(x => x.password.Equals(password)).ToList();
                if (check.Count == 0)
                {
                    ViewBag.error = "Incorrect password";
                    return RedirectToAction("Login");
                }
                var data1 = _db.Accounts.Where(x => x.username.Equals(username) & x.password.Equals(password));
                var data2 = _db.Accounts.Where(x => x.email.Equals(username) & x.password.Equals(password));
                if ((data1.Count() != 0))
                {
                    Session["username"] = data1.FirstOrDefault().username;
                    Session["id"] = data1.FirstOrDefault().id;

                    ViewBag.Message = data1.FirstOrDefault().username + " Successfully registerd.";
                    return RedirectToAction("Index", "Home");
                }
                else if ((data2.Count() != 0))
                {
                    Session["username"] = data2.FirstOrDefault().email;
                    Session["id"] = data1.FirstOrDefault().id;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.mess_err = "Incorrect account or password information!!!";
                    return RedirectToAction("Login");
                }
            }


            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

    }
}