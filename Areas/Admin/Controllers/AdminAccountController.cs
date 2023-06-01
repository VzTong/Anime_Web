﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anime_Web.Models;
using System.Security.Cryptography;
using System.Text;

namespace Anime_Web.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        // GET: Admin/AdminAccount
        private readonly WEB_Anime_ASPEntities1 _db = new WEB_Anime_ASPEntities1();

        [HttpGet]
        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegister(AccountM _Admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check user_name 
                    var check = _db.Accounts.FirstOrDefault(s => s.username == _Admin.username);

                    if (check == null)
                    {
                        _Admin.password = GetMD5(_Admin.password);
                        _db.Configuration.ValidateOnSaveEnabled = false;

                        var account = new Account
                        {
                            email = _Admin.email,
                            username = _Admin.username,
                            password = _Admin.password,
                            ischeck = 1
                        };
                        _db.Accounts.Add(account);
                        _db.SaveChanges();

                        ModelState.Clear();
                        ViewBag.Message = account.username + " Successfully registerd.";
                        return RedirectToAction("AdminLogin", "AdminAccount");
                    }
                    else
                    {
                        ViewBag.error = "Username already exists";
                        return View();
                    }
                }
                else
                {
                    return View();
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
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string adminname, string password)
        {
            if (ModelState.IsValid)
            {
                var f_pass = GetMD5(password);
                var check = _db.Accounts.Where(x => x.password.Equals(f_pass)).ToList();
                if (check.Count == 0)
                {
                    //ViewBag.error = "mật khẩu sai";
                    ViewBag.mess_err = "tài khoản hoặc mật khẩu không chính xác";
                    return RedirectToAction("AdminLogin");
                }
                var data = _db.Accounts.Where(x => x.username.Equals(adminname) && x.password.Equals(f_pass));
                if ((data.Count() != 0))
                {
                    Session["username"] = data.FirstOrDefault().username;
                    Session["id"] = data.FirstOrDefault().id;

                    ViewBag.Message = data.FirstOrDefault().username + " Successfully registerd.";
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    ViewBag.mess_err = "tài khoản hoặc mật khẩu không chính xác";
                    return RedirectToAction("AdminLogin");
                }
            }


            return View();
        }

        public ActionResult AdminLogout()
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